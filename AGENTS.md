# AGENTS — Radar Ident QuickSwitch internals

LLM and contributor map for the BepInEx client plugin. Install and operator steps live in [HUMANS.md](HUMANS.md).

## Stack

| Layer | Choice |
| --- | --- |
| Language | C# (`netstandard2.1`, `LangVersion=latest`) |
| Host | Lethal Company (Unity 2022.3) via BepInEx 5.4 |
| Hooking | MonoMod `On.*` detours (not Harmony) |
| UI labels | TextMeshPro billboards parented to map dots |
| Packaging | Thunderstore (`Thunderstore/manifest.json`) |

## Entrypoints

| Path | Role |
| --- | --- |
| `Radar Ident QuickSwitch.sln` | Solution entry |
| `PlayerMapNumbers/Plugin.cs` | BepInEx plugin, config, MonoMod hooks, radar label logic |
| `PlayerMapNumbers/Radar Ident QuickSwitch.csproj` | Build output `com.lethalmodding.radar_ident_quickswitch.dll` |
| `Thunderstore/manifest.json` | Thunderstore metadata, dependencies, incompatibilities |
| `Thunderstore/config/*.cfg` | Default BepInEx config shipped with releases |
| `docs/install.md` | Operator install detail (linked from HUMANS) |

The `PlayerMapNumbers/` folder name is historical; the assembly and GUID use `radar_ident_quickswitch`.

## Architecture

```
Plugin.Awake
  ├─ LoadConfig (BepInEx ConfigFile)
  ├─ ManualCameraRenderer hooks → TrackAllRadarTargets / StartTracking / StopTracking
  ├─ PlayerControllerB hooks → refresh labels on join/death
  └─ Terminal.ParsePlayerSentence → digit + Enter quick-switch
```

`StartTracking` assigns sequential numeric identifiers, spawns a `MapNumber` TextMeshPro child on the map dot, and stores bidirectional maps (`playerAssignments`, `playerMapLabels`).

## Invariants

- Plugin GUID: `com.lethalmodding.radar_ident_quickswitch` (`MyPluginInfo` from BepInEx.PluginInfoProps).
- Requires **MMHOOK_Assembly-CSharp** at runtime; game and BepInEx DLL references in the csproj are developer-local HintPaths.
- Thunderstore declares incompatibility with `dslogget-PlayerMapNumbers` — do not remove without product intent.
- `configGeneralEnabled` gates all tracking and terminal parsing; respect it in new hooks.
- Terminal quick-switch only handles a single integer token; multi-digit and non-numeric identifiers are future work (see commented config blocks in `Plugin.cs` and [ROADMAP.md](ROADMAP.md)).
- Build: `dotnet restore "Radar Ident QuickSwitch.sln"` (see `.gate.toml`); copy built DLL into a BepInEx profile for manual testing.
- Do not add install or operator walkthroughs here — [HUMANS.md](HUMANS.md) owns run/use docs.

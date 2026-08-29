# AGENTS — Radar Ident QuickSwitch internals

BepInEx plugin map. Run/use: [HUMANS.md](HUMANS.md).

## Stack

| Layer | Choice |
| --- | --- |
| Language | C# `netstandard2.1` |
| Host | Lethal Company (Unity 2022.3), BepInEx 5.4 |
| Hooks | MonoMod `On.*` detours |
| UI | TextMeshPro on map dots |
| Ship | Thunderstore `Thunderstore/manifest.json` |

## Entrypoints

| Path | Role |
| --- | --- |
| `Radar Ident QuickSwitch.sln` | Solution |
| `PlayerMapNumbers/Plugin.cs` | Plugin, config, hooks, labels |
| `PlayerMapNumbers/Radar Ident QuickSwitch.csproj` | → `com.lethalmodding.radar_ident_quickswitch.dll` |
| `Thunderstore/` | manifest, default config |
| `docs/install.md` | Install detail (HUMANS links here) |

`PlayerMapNumbers/` folder name is historical; GUID is `radar_ident_quickswitch`.

## Invariants

- GUID `com.lethalmodding.radar_ident_quickswitch` (`MyPluginInfo`).
- Runtime needs **MMHOOK_Assembly-CSharp**; game DLL refs are dev-local HintPaths.
- Thunderstore incompatibility `dslogget-PlayerMapNumbers` — intentional.
- `configGeneralEnabled` gates tracking and terminal parsing.
- Quick-switch: single integer token only (multi-digit deferred — see `Plugin.cs`, [ROADMAP.md](ROADMAP.md)).
- Build: `dotnet restore "Radar Ident QuickSwitch.sln"`; manual test = copy DLL to BepInEx profile.
- No operator walkthroughs here — [HUMANS.md](HUMANS.md).

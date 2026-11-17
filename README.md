<h1 align="center">Radar Ident QuickSwitch</h1>

<div align="center">

[![Thunderstore](https://img.shields.io/badge/Thunderstore-Radar%20Ident%20QuickSwitch-blue)](https://thunderstore.io/c/lethal-company/p/LethalModding/Radar_Ident_QuickSwitch/)

</div>

---

Radar Ident QuickSwitch lets terminal operators switch radar targets with a single-digit identifier by default — faster QoL for the ship terminal.

Compatible with game v40 or v45; tested with MoreCompany.

## Quick start

```text
# r2modman / Thunderstore Mod Manager: search "Radar Ident QuickSwitch"
```

Install details are in [docs/install.md](docs/install.md); configuration, usage and uninstalling are below.

## Features

- Quick radar target switching via numeric IDs
- Custom identifier length (planned)

## Configuration

The config file is created on first run at `BepInEx/config/com.lethalmodding.radar_ident_quickswitch.cfg`. The only setting is `Enabled` in the `General` section (default `true`), which switches both the radar labels and the terminal parsing on or off. The shipped defaults live in [Thunderstore/config](Thunderstore/config/com.lethalmodding.radar_ident_quickswitch.cfg).

## Usage

Every client that wants matching labels needs the mod. Open the radar view on the terminal, find the digit shown over a target, then type it and press Enter. That does the same thing as `switch <name>`. Only a single integer is parsed for now; see [ROADMAP.md](ROADMAP.md).

To check the install, look for `BepInEx/plugins/com.lethalmodding.radar_ident_quickswitch.dll` in your profile and for the plugin's load lines in `BepInEx/LogOutput.log`. The radar view should show green digits.

To uninstall, remove the DLL (or disable the mod in your manager) and delete the config file if you no longer want it.

PlayerMapNumbers assigns radar IDs too, so the two cannot be installed together.

## Building

The plugin is C# targeting `netstandard2.1` for Lethal Company (Unity 2022.3) on BepInEx 5.4, and hooks the game with MonoMod `On.*` detours. Labels are TextMeshPro text on the map dots.

- `PlayerMapNumbers/Plugin.cs` holds the plugin, config, hooks and labels. The folder name is historical; the plugin GUID is `com.lethalmodding.radar_ident_quickswitch`.
- Run `dotnet restore "Radar Ident QuickSwitch.sln"`. Game DLL references are HintPaths to your local install.
- At runtime the plugin needs MMHOOK_Assembly-CSharp.
- To test by hand, copy the built DLL into a BepInEx profile.
- `Thunderstore/` has the manifest and default config used for releases. The manifest marks `dslogget-PlayerMapNumbers` as incompatible on purpose.

## Documentation

| Document | Description |
| --- | --- |
| [docs/install.md](docs/install.md) | Install paths and dependencies |
| [CHANGELOG.md](CHANGELOG.md) | Release notes |
| [ROADMAP.md](ROADMAP.md) | Planned features |
| [LICENSE.md](LICENSE.md) | MIT license |

## License

MIT — see [LICENSE.md](LICENSE.md). Upstream reference: MIT [PlayerMapNumbers](https://thunderstore.io/c/lethal-company/p/dslogget/PlayerMapNumbers/) by dslogget.

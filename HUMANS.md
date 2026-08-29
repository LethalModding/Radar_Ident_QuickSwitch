# HUMANS.md — Run and use

Install, configure, and use Radar Ident QuickSwitch. Internals: [AGENTS.md](AGENTS.md).

## Quick start

Install via r2modman or Thunderstore Mod Manager, join as terminal operator, type a radar digit and press Enter.

```text
# r2modman / Thunderstore Mod Manager: search "Radar Ident QuickSwitch"
```

## Install

See [docs/install.md](docs/install.md). Incompatible with [PlayerMapNumbers](https://thunderstore.io/c/lethal-company/p/dslogget/PlayerMapNumbers/) — both assign radar IDs.

## Environment

Config after first run: `BepInEx/config/com.lethalmodding.radar_ident_quickswitch.cfg`

| Section | Key | Default | Effect |
| --- | --- | --- | --- |
| General | Enabled | `true` | Master switch |

Defaults: [Thunderstore/config/com.lethalmodding.radar_ident_quickswitch.cfg](Thunderstore/config/com.lethalmodding.radar_ident_quickswitch.cfg).

## Usage

1. All clients that need matching labels must run the mod.
2. Terminal radar view — digits appear over targets.
3. Type digit + **Enter** to select (same as `switch <name>`).

Tested on v40/v45 with MoreCompany.

## Verify

1. `BepInEx/plugins/com.lethalmodding.radar_ident_quickswitch.dll` in profile.
2. `BepInEx/LogOutput.log` shows plugin load lines.
3. Radar view shows green digits; digit + Enter changes selection.

## Uninstall

Remove plugin DLL (or disable in mod manager); delete config file if unwanted.

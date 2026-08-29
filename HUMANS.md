# HUMANS.md — Run and use

Operator guide for installing, configuring, and using Radar Ident QuickSwitch in Lethal Company. For repository layout and plugin internals, see [AGENTS.md](AGENTS.md).

## Quick start

Install via [r2modman](https://github.com/ebkr/r2modman) or Thunderstore Mod Manager, then join a lobby as terminal operator. Digits appear over radar targets; type a digit and press Enter to switch.

```text
# In r2modman or Thunderstore Mod Manager: search "Radar Ident QuickSwitch" and install
```

## Install

Full install paths, manual extraction, and dependencies are in [docs/install.md](docs/install.md).

**Summary:** install [BepInExPack 5.4.2100+](https://thunderstore.io/c/lethal-company/p/BepInEx/BepInExPack/) before manual installs. r2modman and Thunderstore Mod Manager pull dependencies automatically.

**Incompatible mod:** do not run alongside [PlayerMapNumbers](https://thunderstore.io/c/lethal-company/p/dslogget/PlayerMapNumbers/) — both assign radar identifiers.

## Environment

BepInEx writes config after first run:

`BepInEx/config/com.lethalmodding.radar_ident_quickswitch.cfg`

| Section | Key | Default | Effect |
| --- | --- | --- | --- |
| General | Enabled | `true` | Master switch; when `false`, hooks and labels are inactive |

Shipped defaults also live in [Thunderstore/config/com.lethalmodding.radar_ident_quickswitch.cfg](Thunderstore/config/com.lethalmodding.radar_ident_quickswitch.cfg).

## Usage

1. Host or join a lobby with the mod enabled on all clients that need matching radar labels.
2. Open the ship terminal and switch to the radar view.
3. Note the digit above each radar target icon.
4. Type the target digit and press **Enter** to select that target (same outcome as `switch <name>`).

The mod targets game builds **v40** and **v45** and is tested with MoreCompany.

## Verify

1. After install, confirm `BepInEx/plugins/com.lethalmodding.radar_ident_quickswitch.dll` exists in the profile.
2. Launch Lethal Company; check `BepInEx/LogOutput.log` for `com.lethalmodding.radar_ident_quickswitch` load lines.
3. In a lobby, open terminal radar view — green digits should appear over player and booster dots.
4. Type `1` (or another visible digit) and press Enter; the radar selection should change to that target.

## Uninstall

Remove the plugin DLL from `BepInEx/plugins/` (or disable the mod in your mod manager) and delete `BepInEx/config/com.lethalmodding.radar_ident_quickswitch.cfg` if you no longer need saved settings.

# SPT.NoScavSelect

[![Downloads](https://img.shields.io/github/downloads/McReaper/SPT.NoScavSelect/total)](https://github.com/McReaper/SPT.NoScavSelect/releases)

Client mod for **SPT 4.1.6** that skips the PMC/Scav selection screen: pressing **Play** takes you straight to map selection as a PMC.

Port of [vibrantrida/no-scav-select](https://gitlab.com/vibrantrida/no-scav-select) (0BSD) to SPT 4.x.

## Install

Drop `NoScavSelect.dll` into `<SPT>/BepInEx/plugins/`.

## What changed in the port

- The original postfixed `MatchMakerSideSelectionScreen.Awake` and called the screen's own handlers
  (`method_17` / `method_18`: pick PMC, press Next). In SPT 4.1.6 those handlers are named `CG_Awake4` / `CG_Awake5`.
- The port instead prefixes `MainMenuShowOperation.method_44` (the method that opens the side selection screen),
  sets `RaidSettings.Side = Pmc` and jumps straight to `method_45` (the map selection step), the same thing the Next button does.
  The screen is never built, so:
  - it is skipped every time you press Play, not only on the first `Awake`;
  - **Back** on the map screen returns to the main menu instead of the side selection screen.
- Trimmed references, removed the `.vs` folder, and made the SPT install path configurable.

## Build

Requires the .NET SDK and an SPT 4.1.6 install.

```sh
dotnet build -c Release -p:SPTPath="C:\SPT"
# add -p:InstallToSPT=true to copy the DLL into BepInEx/plugins
```

If the repo sits in `<SPT>/Development/SPT.NoScavSelect`, `SPTPath` can be omitted.

Note: `method_44` / `method_45` are obfuscated names. They will likely change with the next EFT/SPT version.

## License

[0BSD](LICENSE.md), same as the original.

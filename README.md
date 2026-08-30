# 🚀 MIO Shield
A mod for the game Hollow Knight: Silksong that grants the player a shield like in the game MIO: Memories in Orbit.

---

## 🛠 Features

- When the player takes a hit, the shield is sacrificed to prevent damage.
- If the player's Fractured Mask would break from damage when the shield is on, it will be prevented.
- The shield will recover after 5 seconds or if the player sits at a bench.
- The shield will also prevent consecutive damage (Driznit drills, Muckroach pounces etc).

## 📦 Installation

Prerequisites: This mod requires `BepInEx`.

## 🚀 Usage
- Create a new `Directory.Build.targets` file inside the project directory:
```xml
<Project>
    <PropertyGroup>
        <SILKSONG_PATH>/home/your-name/snap/steam/common/.local/share/Steam/steamapps/common/Hollow Knight Silksong</SILKSONG_PATH>
    </PropertyGroup>
    <PropertyGroup>
        <DEBUG>true</DEBUG>
    </PropertyGroup>
</Project>

```
- Directory schema:
```
MioShield
    ├ MioShield.csproj
    └ Directory.Build.targets
```
- Run with `dotnet build`
- Place `MioShield.dll` inside `/Hollow Knight Silksong/BepInEx/plugins`
```
Hollow Knight Silksong
├ Hollow Knight Silksong_Data
└ BepInEx
     └ plugins
          ├ MioShield.dll
          └ Config
              └ config.json
```
- Launch the game

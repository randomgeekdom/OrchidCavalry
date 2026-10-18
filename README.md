# Orchid Cavalry

Orchid Cavalry is a .NET MAUI game (CommunityToolkit.Mvvm) in which the player leads the Orchid Cavalry, managing characters, cities, factions, and quests turn by turn.

This document describes only what exists in the code. It does not invent or describe planned game content.

## What exists in the code

**Game state** (`OrchidCavalry.Domain.Game`)
- Characters, cities, factions, quests, the current commander, completed/successfully-completed quest IDs, the number of turns taken, and alerts displayed between turns.
- Helpers to add/remove characters, cities, factions, and quests; to look up cities and factions by name; to adjust faction reputation; and to replace the leader with the highest-ranked, highest-skilled remaining member.
- A derived `GetScore()` (plus per-component accessors) sums successfully-completed quests, total faction reputation, character victories, and turns survived.

**Characters** (`Character`)
- First/last name, titles (including "Orchid Cavalry Commander"), skills with values plus enhanced/debilitated state, victories/defeats, and a rank derived from victories (Recruit through General).
- Skills can be enhanced, debilitated, or have their values adjusted (total skill value is capped at 35).

**Cities** (`City`)
- Name, biome, population, and the ID of the ruling faction.

**Factions** (`Faction`)
- Name and an Orchid Cavalry reputation value.

**Quests** (`Quest` abstract base; `MarauderQuest`, `MonsterQuest`, and unique quests)
- Title, description, city, mandatory/optional character slots, expiration in turns, a loot list, and skill-based resolutions.
- Resolving a quest rolls dice against character skills. Results can add titles, award a victory, modify city faction reputation, injure or kill characters, debilitate/enhance skills, or trigger a quest fail state.
- Unresolved quests expire one turn at a time.

**Dice** (`IDiceRoller` / `DiceRoller`)
- d20-based roll with enhanced/debilitated advantage/disadvantage, mapping results to Catastrophe, Fail, MinorSuccess, Success, or PerfectSuccess.

**Turn flow** (`GameplayService.NextTurnAsync`)
- Advances the turn counter, evaluates every active quest, generates new quests for undeployed characters, conscripts new members when the cavalry falls below 8, and replaces the commander if the current one is gone. Each turn ends by saving the game.

**Saving** (`GameSaver`, `IGameSaver`)
- Serializes the game to `orchid.sav` in the app data directory using Newtonsoft.Json (`TypeNameHandling.Auto`), keeping a `.backup` of the previous save.

**UI** (.NET MAUI)
- `MainPage`: start/exit.
- `Dashboard`: commander, available quests, alerts, the current score breakdown, and the end-turn action.
- `CharacterView`, `QuestView`, `CitiesView`, `FactionView`, `ChoiceView`.
- Popup and view-model layer (`ViewModels/*`, `Services/*PopupService.cs`), wired up in `Registrations.cs` and `MauiProgram.cs`.

## Solution structure

| Project | Purpose | Target framework |
|---|---|---|
| `OrchidCavalry.Domain` | Domain models (game, character, city, faction, quest) and domain services (dice roller, unique-quest retrieval) | net8.0 |
| `OrchidCavalry.Services` | Application services (gameplay, character generation, city service, quest generation) | net8.0 |
| `OrchidCavalry` | MAUI app: views, view models, popups, service registration | net8.0-android, net8.0-ios, net8.0-maccatalyst, net8.0-windows10.0.19041.0 |
| `OrchidCavalry.Services.Tests` | xUnit tests covering Domain and Services | net8.0 |

Dependency direction: App → Services → Domain.

Also present on disk but not referenced by the solution: `OrchidCavalry.Models` and `OrchidCavalry.Models.Tests`.

Domain model types are declared primarily under the `OrchidCavalry.Models` namespace, with some types under `OrchidCavalry.Domain.*` namespaces.

## How to build and run

Prerequisites: .NET SDK with the MAUI workloads installed.

Build the class libraries:
```
dotnet build OrchidCavalry.Domain\OrchidCavalry.Domain.csproj
dotnet build OrchidCavalry.Services\OrchidCavalry.Services.csproj
```

Build the whole solution:
```
dotnet build OrchidCavalry.sln
```

Run the tests:
```
dotnet test OrchidCavalry.sln
```

Run the app on Windows:
```
dotnet run --project OrchidCavalry\OrchidCavalry.csproj -f net8.0-windows
```

## Known state (from the code)

- There is no game-over condition by design: the game is an endless sandbox, and turns continue even if all characters die or all cities fall.
- Quest `Loot` lists are never populated, so quests award no loot today.
- 9 of the 15 defined skills are unused in quest resolution.
- Progress is expressed as a score (successfully-completed quests, faction reputation, character victories, and turns survived) rather than a win/loss state.

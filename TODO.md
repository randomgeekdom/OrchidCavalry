# OrchidCavalry TODO

> ## ⚠️ GOVERNANCE - READ BEFORE ANY WORK
>
> **Do NOT take large-scale actions without asking the user first.**
>
> The user dictates the direction of the game. No new features, no new quests,
> no balance changes, no big refactors, no deletions, no architecture changes
> without explicit approval. Items tagged **[large]** require the user's
> explicit OK before starting. When in doubt, ask.

## Bug fixes

- [ ] **[small] Commander is never replaced** - `GameplayService.ReplaceCommanderIfNecessaryAsync` calls `game.ReplaceLeader()` but throws away the result. `game.Commander` still points at the dead character, and the "A New Commander" alert names the corpse. Assign the return value (and handle the case where no members remain).
- [ ] **[small] Marauder quest can crash the game** - `MarauderQuest.EvaluateFailStateAsync` catastrophe branch calls `game.GetFactionByName(BandName)`, which throws if that band's faction was never added to `game.Factions` (factions are only added when cities are created). Look it up safely or add the faction on demand.
- [ ] **[small] Quests can target already-threatened cities** - `CityService.GetUnthreatenedRandomCityAsync` filters for unthreatened cities but then indexes the full `game.Cities` list, so the filter does nothing; also, with exactly one unthreatened city it always invents a new city instead of using it. Index into the filtered list.
- [ ] **[small] Characters can die after a quest is already won** - `Quest.EvaluateQuestAsync` keeps rolling for every remaining character slot after a success, because the `while` loop only re-checks after the `foreach` finishes. Lower-ranked characters can be killed in a fight that was already won. Exit the loop once the quest is resolved.
- [ ] **[small] Dead `Play.xaml` reference** - `OrchidCavalry.csproj` contains `<MauiXaml Update="Views\Play.xaml">` for a file that does not exist. Remove the leftover.
- [ ] **[small] Copy-pasted doc comment** - `MarauderQuest`'s summary says "stop a monster from terrorizing a city". It should say marauders.
- [ ] **[small] Sloppy save-backup path** - `GameSaver.SaveGameAsync` builds the backup with `Path.Combine(FileSystem.AppDataDirectory, filePath + ".backup")` even though `filePath` is already absolute. Simplify to `filePath + ".backup"`.

## Necessary changes

- [ ] **[medium] Add README.md** - what the game is, architecture, how to build and run.
- [ ] **[medium] Fill in LICENSE.txt** - replace the `[year]` / `[fullname]` placeholders (MIT, randomgeekdom).
- [ ] **[medium] Replace placeholder tests** - `OrchidCavalry.Services.Tests` contains only `UnitTest1`; add real coverage (dice roller, quest resolution, commander replacement, faction reputation).

## Large-scale - REQUIRES USER APPROVAL

- [ ] **[large] Delete the dead `OrchidCavalry.Models` project** - nothing in the app, Domain, or Services references it (only its own test project does); every type it contains is re-declared in `OrchidCavalry.Domain`. Deleting a whole project needs the user's OK.
- [ ] **[large] Fix namespaces repo-wide** - the Domain project declares its types in `namespace OrchidCavalry.Models`. Rename to `OrchidCavalry.Domain` across all files. Mechanical, but touches everything.
- [ ] **[large] Define a game-over condition** - nothing ends the game today: if all characters die or all cities fall, turns keep running. This is a design decision - the user decides what "game over" means before it is implemented.
- [ ] **[large] New quests, skills, factions, or balance changes** - the user designs the game; these come from the user, not from `/next`.

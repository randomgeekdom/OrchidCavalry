# OrchidCavalry TODO

> ## ⚠️ GOVERNANCE - READ BEFORE ANY WORK
>
> **The user is the sole creative authority. Do NOT make creative decisions, and do NOT take large-scale actions, without asking the user first.**
>
> All creative aspects - game design, how the game is played, mechanics, rules, balance, quests, characters, factions, skills, names, flavor, story, tone, direction - belong to the user. The agent is an instrument of the user's implementation and may only offer suggestions for the user to consider.
>
> Items tagged **[large]**, and any item whose fix requires choosing how the game plays, require the user's explicit OK before starting. When in doubt, ask.

## Bug fixes

- [x] **[small] Commander is never replaced** - `ReplaceCommanderIfNecessaryAsync` now assigns `game.Commander = game.ReplaceLeader()` (handling no members remaining with a distinct alert). Added test coverage in `GameplayServiceTests`.
- [ ] **[small] Marauder quest can crash the game** - `MarauderQuest.EvaluateFailStateAsync` catastrophe branch calls `game.GetFactionByName(BandName)`, which throws if that band's faction was never added to `game.Factions` (factions are only added when cities are created). Look it up safely or add the faction on demand.
- [ ] **[small] Quests can target already-threatened cities** - `CityService.GetUnthreatenedRandomCityAsync` filters for unthreatened cities but then indexes the full `game.Cities` list, so the filter does nothing; also, with exactly one unthreatened city it always invents a new city instead of using it. Index into the filtered list.
- [ ] **[small] Characters can die after a quest is already won** - `Quest.EvaluateQuestAsync` keeps rolling for every remaining character slot after a success, because the `while` loop only re-checks after the `foreach` finishes. Lower-ranked characters can be killed in a fight that was already won. Exit the loop once the quest is resolved.
- [ ] **[small] Dead `Play.xaml` reference** - `OrchidCavalry.csproj` contains `<MauiXaml Update="Views\Play.xaml">` for a file that does not exist. Remove the leftover.
- [ ] **[small] Copy-pasted doc comment** - `MarauderQuest`'s summary says "stop a monster from terrorizing a city". It should say marauders.
- [ ] **[small] Sloppy save-backup path** - `GameSaver.SaveGameAsync` builds the backup with `Path.Combine(FileSystem.AppDataDirectory, filePath + ".backup")` even though `filePath` is already absolute. Simplify to `filePath + ".backup"`.

## Necessary changes

- [x] **[medium] Add README.md** - document what exists in the code only (what the game is, architecture, how to build and run). Do not invent or embellish game content; if describing the game requires creative wording, ask the user. - Added `README.md` covering the game as implemented (game state, characters, cities, factions, quests, dice, turn flow, saving, UI), solution structure/project table, architecture notes, and build/run/test commands. Domain and Services build cleanly; MAUI app could not be built here because restore fails (`$(MauiVersion)` undefined; net8.0 workloads EOL under the .NET 10 preview SDK), a pre-existing condition unrelated to this change.
- [x] **[medium] Fill in LICENSE.txt** - replaced the `[year]` / `[fullname]` placeholders with `2026 randomgeekdom` (per the TODO note); MIT text unchanged. Domain and Services build cleanly; MAUI app still cannot build (pre-existing net8.0 workload EOL / undefined `$(MauiVersion)`, unrelated to this change).
- [x] **[medium] Replace placeholder tests** - Replaced `UnitTest1` with real xunit coverage: dice roller output, quest resolution (success & death), commander replacement via `GameplayService.NextTurnAsync`, and faction reputation. Added project references to Domain/Services and retargeted test project to net8.0; smoke-tested 13 passing tests.
- [ ] **[medium] Retarget projects to .NET 10** - move Domain, Services, Services.Tests (and app if workloads permit) from net8.0 to net10.0 and verify build/test.

## Large-scale - REQUIRES USER APPROVAL

- [ ] **[large] Delete the dead `OrchidCavalry.Models` project** - nothing in the app, Domain, or Services references it (only its own test project does); every type it contains is re-declared in `OrchidCavalry.Domain`. Deleting a whole project needs the user's OK.
- [ ] **[large] Fix namespaces repo-wide** - the Domain project declares its types in `namespace OrchidCavalry.Models`. Rename to `OrchidCavalry.Domain` across all files. Mechanical, but touches everything.
- [x] **[large] Define a game-over condition** - User decided: **endless sandbox, no game-over**. Instead of a loss state, the game now tracks a derived score shown on the dashboard: successfully-completed quests + total faction reputation + character victories + turns survived. Implemented `Game.TurnCount` and `Game.SuccessfulQuestIds` (only won quests), `Game.GetScore()`/per-component accessors; `Quest.EvaluateQuestAsync` now awards a victory via `Character.AddVictory()` on each successful check and records the quest as successful; `GameplayService.NextTurnAsync` advances `TurnCount`. Added `ScoreTests` (win/loss/sum/turn-count). Domain + Services build and all 17 tests pass; MAUI app still cannot restore (pre-existing net8.0 workload/`MauiVersion` issue), so the Dashboard label change is unbuilt here.
- [ ] **[large] New quests, skills, factions, or balance changes** - the user designs the game; these come from the user, not from `/next`.

## 💡 Agent suggestions - awaiting the user's creative decision

These are suggestions from the agent, not decisions. The user owns all of them. `/next` must never pick these.

- [ ] **[design]** City population growth is dead code - `CityService.IncreasePopulationAsync` is never called. User to decide: wire it in as end-of-turn growth, or remove it.
- [ ] **[design]** Loot system is a blank canvas - `Quest.Loot` is never populated, so quests award nothing. User to design what loot is and how it is earned.
- [ ] **[design]** 9 of 15 skills are unused - only Aura, Melee, Target, Influence, Deduction, and Movement appear in quest resolutions. User to decide what Brawn, Assist, Destiny, Knowledge, Resilience, Sense, Tinker, Wealth, and Willpower do.
- [ ] **[design]** Post-victory behavior - when a quest is won, should the remaining assigned characters keep acting (and risk death), or should the fight end immediately?
- [ ] **[design]** Faction reputation effects - factions only track reputation; should reputation thresholds trigger effects (e.g., allied factions send aid)?

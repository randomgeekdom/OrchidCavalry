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

- [ ] **[medium] Add README.md** - document what exists in the code only (what the game is, architecture, how to build and run). Do not invent or embellish game content; if describing the game requires creative wording, ask the user.
- [ ] **[medium] Fill in LICENSE.txt** - replace the `[year]` / `[fullname]` placeholders (MIT, randomgeekdom).
- [x] **[medium] Replace placeholder tests** - Replaced `UnitTest1` with real xunit coverage: dice roller output, quest resolution (success & death), commander replacement via `GameplayService.NextTurnAsync`, and faction reputation. Added project references to Domain/Services and retargeted test project to net8.0; smoke-tested 13 passing tests.
- [ ] **[medium] Retarget projects to .NET 10** - move Domain, Services, Services.Tests (and app if workloads permit) from net8.0 to net10.0 and verify build/test.

## Large-scale - REQUIRES USER APPROVAL

- [ ] **[large] Delete the dead `OrchidCavalry.Models` project** - nothing in the app, Domain, or Services references it (only its own test project does); every type it contains is re-declared in `OrchidCavalry.Domain`. Deleting a whole project needs the user's OK.
- [ ] **[large] Fix namespaces repo-wide** - the Domain project declares its types in `namespace OrchidCavalry.Models`. Rename to `OrchidCavalry.Domain` across all files. Mechanical, but touches everything.
- [ ] **[large] Define a game-over condition** - nothing ends the game today: if all characters die or all cities fall, turns keep running. This is a design decision - the user decides what "game over" means before it is implemented. Options suggested: commander dies with no successor / all cities fall / endless sandbox with a score.
- [ ] **[large] New quests, skills, factions, or balance changes** - the user designs the game; these come from the user, not from `/next`.

## 💡 Agent suggestions - awaiting the user's creative decision

These are suggestions from the agent, not decisions. The user owns all of them. `/next` must never pick these.

- [ ] **[design]** City population growth is dead code - `CityService.IncreasePopulationAsync` is never called. User to decide: wire it in as end-of-turn growth, or remove it.
- [ ] **[design]** Loot system is a blank canvas - `Quest.Loot` is never populated, so quests award nothing. User to design what loot is and how it is earned.
- [ ] **[design]** 9 of 15 skills are unused - only Aura, Melee, Target, Influence, Deduction, and Movement appear in quest resolutions. User to decide what Brawn, Assist, Destiny, Knowledge, Resilience, Sense, Tinker, Wealth, and Willpower do.
- [ ] **[design]** Post-victory behavior - when a quest is won, should the remaining assigned characters keep acting (and risk death), or should the fight end immediately?
- [ ] **[design]** Faction reputation effects - factions only track reputation; should reputation thresholds trigger effects (e.g., allied factions send aid)?

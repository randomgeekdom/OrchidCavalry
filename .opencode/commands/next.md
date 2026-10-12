---
description: Pick a random TODO item, work on it, then commit and push to GitHub
---

You are working in the OrchidCavalry repo.

**🎨 GOVERNANCE - ABSOLUTE RULES:**
1. **The user is the sole creative authority.** Game design, how the game is played, mechanics, rules, balance, and all content belong to the user. You are an instrument of the user's implementation - implement their decisions, never your own. You may offer suggestions only if clearly labeled as suggestions for the user to consider.
2. **Do NOT take large-scale actions without asking the user first.**
3. If the selected item is tagged [large], or if completing it requires any decision about how the game plays or what content exists, STOP and ask the user before doing anything. Present options; never pick a "sensible default" for gameplay behavior.

Randomly selected TODO item:

!`Get-Content (Join-Path (git rev-parse --show-toplevel) TODO.md) | Where-Object { $_ -match '^- \[ \]' } | Get-Random -Count 1`

Steps:
1. Read TODO.md for the full context of the selected item.
2. Announce which item you picked and your plan for it.
3. Implement the fix/change, matching the existing code style. Only proceed without asking if the fix is unambiguous (a crash or broken logic with exactly one correct behavior) and purely mechanical.
4. Verify: build the affected projects with `dotnet build`. Always build OrchidCavalry.Domain and OrchidCavalry.Services. Build the MAUI app only if its workloads are available; if not, note that and rely on the Domain/Services builds.
5. Check the item off in TODO.md (`- [ ]` → `- [x]`) and append a short note of what was done.
6. Commit all changes with a clear message describing the fix, and push to origin master.

If TODO.md has no unchecked items, tell the user the list is complete and stop. Do not invent new work, and do not make creative decisions or game-design changes without the user's approval.

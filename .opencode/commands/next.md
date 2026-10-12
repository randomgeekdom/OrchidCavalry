---
description: Pick a random TODO item, work on it, then commit and push to GitHub
---

You are working in the OrchidCavalry repo.

**⚠️ GOVERNANCE: Do NOT take any large-scale actions without asking the user first. The user dictates the direction of the game.** Items tagged [large] in TODO.md require the user's explicit approval — if the random pick is [large], present it to the user and ask before doing anything. If the fix turns out to be bigger than the item describes, STOP and ask before continuing.

Randomly selected TODO item:

!`Get-Content (Join-Path (git rev-parse --show-toplevel) TODO.md) | Where-Object { $_ -match '^- \[ \]' } | Get-Random -Count 1`

Steps:
1. Read TODO.md for the full context of the selected item.
2. Announce which item you picked and your plan for it.
3. Implement the fix/change, matching the existing code style.
4. Verify: build the affected projects with `dotnet build`. Always build OrchidCavalry.Domain and OrchidCavalry.Services. Build the MAUI app only if its workloads are available; if not, note that and rely on the Domain/Services builds.
5. Check the item off in TODO.md (`- [ ]` → `- [x]`) and append a short note of what was done.
6. Commit all changes with a clear message describing the fix, and push to origin master.

If TODO.md has no unchecked items, tell the user the list is complete and stop. Do not invent new work, and do not start any new features or game-design changes without the user's approval.

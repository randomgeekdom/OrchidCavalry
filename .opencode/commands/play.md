---
description: Build and launch the OrchidCavalry app so the user can play it
---

Launch the OrchidCavalry MAUI app on Windows so the user can play.

1. Build the Windows target. The Windows App SDK must be bundled into the output
   (`WindowsAppSDKSelfContained=true`); without it the app exits immediately with
   `COMException 0x80040154 (REGDB_E_CLASSNOTREG)` while auto-initializing the Windows App SDK.

   ```
   dotnet build OrchidCavalry\OrchidCavalry.csproj -f net10.0-windows10.0.19041.0 -p:WindowsAppSDKSelfContained=true
   ```

2. Start the built executable detached so the session does not block on it:

   ```
   Start-Process "OrchidCavalry\bin\Debug\net10.0-windows10.0.19041.0\win-x64\OrchidCavalry.exe"
   ```

3. Confirm it is running, then tell the user the game window is open:

   ```
   Start-Sleep -Seconds 5; Get-Process OrchidCavalry -ErrorAction SilentlyContinue | Select-Object Id,MainWindowHandle
   ```

Notes:
- If the build fails, report the compiler errors and stop. Do not change game code, content, or balance to make it launch.
- If the process is already running, tell the user instead of starting another copy.

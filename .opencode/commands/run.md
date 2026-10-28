---
description: Build and run the OrchidCavalry app on Windows
---

Build and run the OrchidCavalry MAUI app on Windows.

1. Build the Windows target with the Windows App SDK bundled. This is required:
   a framework-dependent build crashes on startup with `COMException 0x80040154 (REGDB_E_CLASSNOTREG)`.

   ```
   dotnet build OrchidCavalry\OrchidCavalry.csproj -f net10.0-windows10.0.19041.0 -p:WindowsAppSDKSelfContained=true
   ```

2. Launch the built executable in the background (do not block the session):

   ```
   Start-Process "OrchidCavalry\bin\Debug\net10.0-windows10.0.19041.0\win-x64\OrchidCavalry.exe"
   ```

3. Verify it stayed up and report the result:

   ```
   Start-Sleep -Seconds 5; Get-Process OrchidCavalry -ErrorAction SilentlyContinue | Select-Object Id,MainWindowHandle
   ```

If the build fails, report the errors and stop; do not modify game behavior to work around it.

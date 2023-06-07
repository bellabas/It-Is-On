![It is ON!](ItIsOn/appicon.ico)

Prevents the computer from going to sleep. The app was developed with WPF and WinForms in the MVVM pattern, and it uses IoC container.

## Usage:
Three selectable modes are available:
* Prevent Screensaver and Sleep
* Prevent Sleep (which keeps the computer awake but the screensaver will be on)
* Normal mode (the computer state before you opened the app)

### Features:
* The minimize button puts the app in the system tray instead of the normal tray, so it won't bother anyone. A simple click on the tray icon will restore the window.
* Clicking the close button will first safely revert the app back to normal mode then closes the window.

## Dependencies:
* [.NET 5 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

## How it works:
It calls a WinApi method that sets the appropriate flags.

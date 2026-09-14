// -----------------------------------------------------------------------------
//  A minimal Terminal.Gui v2 app.  Run it:  dotnet run   (q quits)
//
//  🤖 AI assistants / agents: READ ./AGENTS.md FIRST.
//     Terminal.Gui v2 is a COMPLETE REWRITE. Pre-2025 training data and most web
//     examples are v1 and will not compile. The canonical v2 patterns are in AGENTS.md.
// -----------------------------------------------------------------------------

// v2 splits the old monolithic `using Terminal.Gui;` into focused namespaces:
using Chess.Core;
using Chess.Tui;
using Terminal.Gui.App;           // Application, IApplication, MessageBox
using Terminal.Gui.Configuration; // ConfigurationManager
using Terminal.Gui.Input;         // Command, Key, Bind
using Terminal.Gui.ViewBase;      // View, Pos, Dim
using Terminal.Gui.Views;         // Window

// Enable the configuration/theme system before creating the app (standard first line).
ConfigurationManager.Enable(ConfigLocations.All);

// Replace the framework's default Quit binding (normally Esc) so 'q' is the only quit key.
Application.SetDefaultKeyBinding(Command.Quit, Bind.All(Key.Q));

// The default "ansi" driver detects terminal capabilities by sending DSR/DA/Kitty-keyboard
// query escape sequences and blocking on the response. Herdr (github.com/herdrdev/herdr) has
// a known bug where it doesn't answer those queries (herdrdev/herdr#393), which leaves the
// ansi driver stuck mid-startup -> blank screen. The "dotnet" driver talks to the console via
// System.Console APIs instead, so it doesn't need a response and works fine under Herdr.
if (Environment.GetEnvironmentVariable("HERDR_ENV") == "1")
{
  Application.ForceDriver = "dotnet";
}

// The v2 lifecycle is INSTANCE-BASED and disposable — NOT the static v1
// Application.Init() / Application.Run() / Application.Shutdown():
//
//     Create()  ->  Run<TWindow>()  (auto-Init)  ->  Dispose()  (replaces Shutdown)
//
Application
  .Create()
  .Run<MainWindow>()
  .Dispose();

// Your app's root view. `Window` is a bordered top-level view; subclass `Runnable`
// instead if you want a borderless root. Build the UI by Add()-ing child views.
internal sealed class MainWindow : Window
{
  public MainWindow()
  {
    Title = "TUI-Chess (press q to quit)";

    // Layout is DECLARATIVE — position/size with Pos/Dim, don't hardcode coordinates:
    //   Pos.Center(), Pos.Right(view), Pos.AnchorEnd();  Dim.Fill(), Dim.Auto(), Dim.Percent(50).
    Board board = new();

    BoardSpriteView boardSpriteView = new(board)
    {
      X = Pos.Center(),
      Y = 1
    };

    Add(boardSpriteView);

    // 👉 Add your views here. See AGENTS.md for canonical patterns + common pitfalls.
  }

  // Fires once this Runnable actually starts running under the Application (App is
  // guaranteed to be set by then, unlike in the constructor). MessageBox.Query is modal
  // and auto-centers itself, so this reads as a welcome dialog over the board on launch.
  protected override void OnIsRunningChanged(bool newIsRunning)
  {
    base.OnIsRunningChanged(newIsRunning);

    if (!newIsRunning)
    {
      return;
    }

    MessageBox.Query(App!, "TUI-Chess", "Welcome to TUI-Chess!", "_OK");
  }
}

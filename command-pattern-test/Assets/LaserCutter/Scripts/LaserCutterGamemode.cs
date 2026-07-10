using Godot;
using System;
using System.Diagnostics;
using LaserCutterUtilities;

public partial class LaserCutterGamemode : Gamemode
{
    public override void _Ready()
    {
        ActiveInputState = new LaserCutterInputState();

        GameActions.TogglePauseMenu += TogglePauseMenuImpl;
    }

    public void TogglePauseMenuImpl()
    {
        GamemodeManager.Instance.UnloadScene();
    }

    public override void _ExitTree()
    {
        GameActions.TogglePauseMenu -= TogglePauseMenuImpl;
    }
}

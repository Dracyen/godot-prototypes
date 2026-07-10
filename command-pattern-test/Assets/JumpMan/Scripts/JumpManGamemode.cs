using Godot;
using System.Diagnostics;
using HordeFighterUtilities;

public partial class JumpManGamemode : Gamemode
{
    public override void _Ready()
    {
        ActiveInputState = new JumpManInputState();

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

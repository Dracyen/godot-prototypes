using Godot;
using System.Diagnostics;
using VoxelBoxUtilities;

public partial class SandboxGamemode : Gamemode
{
    public override void _Ready()
    {
        ActiveInputState = new VoxelBoxInputState();

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
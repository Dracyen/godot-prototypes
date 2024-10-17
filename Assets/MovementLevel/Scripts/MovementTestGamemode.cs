using Godot;
using System.Diagnostics;
using VoxelBoxUtilities;

public partial class MovementTestGamemode : Gamemode
{
    public override void _Ready()
    {
        ActiveInputState = new MovementTestInputState();

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

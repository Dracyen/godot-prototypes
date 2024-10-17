using Godot;
using System;
using MovementTestUtilities;
using System.Diagnostics;

public partial class MovementTestInputState : InputState
{
    public override void Move(Vector2 force)
    {
        if (GameActions.Move != null)
            GameActions.Move(force);
    }

    public override void Cancel()
    {
        Debug.WriteLine("Started Quit");

        if (GameActions.TogglePauseMenu != null)
        {
            GameActions.TogglePauseMenu();
            Debug.WriteLine("Executed Quit");
        }

        Debug.WriteLine("Stopped Quit");
    }
}

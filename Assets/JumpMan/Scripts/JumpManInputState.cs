using Godot;
using System;
using HordeFighterUtilities;

public partial class JumpManInputState : InputState
{
    public override void Move(Vector2 force)
    {
        if (GameActions.Move != null)
            GameActions.Move(force);
    }

    public override void Cancel()
    {
        if (GameActions.TogglePauseMenu != null)
            GameActions.TogglePauseMenu();
    }
}

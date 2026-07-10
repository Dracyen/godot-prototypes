using Godot;
using System;
using LaserCutterUtilities;

public partial class LaserCutterInputState : InputState
{
    public override void Interact1()
    {
        if (GameActions.Fire1 != null)
            GameActions.Fire1();
    }

    public override void Interact2()
    {
        if (GameActions.Fire2 != null)
            GameActions.Fire2();
    }

    public override void AltKey()
    {
        if (GameActions.ToggleEquip != null)
            GameActions.ToggleEquip();
    }

    public override void Elevate(float force)
    {
        if (GameActions.Elevate != null)
            GameActions.Elevate(force);
    }

    public override void Rotate(float force)
    {
        if (GameActions.Rotate != null)
            GameActions.Rotate(force);
    }

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

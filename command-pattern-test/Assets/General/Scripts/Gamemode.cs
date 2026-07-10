using Godot;
using VoxelBoxUtilities;
using System;
using System.Diagnostics;

public partial class Gamemode : Node, InputInterface
{
    public InputState ActiveInputState;

    public override void _Process(double delta)
    {
        if (ActiveInputState == null)
            return;

        Vector2 moveAxis = Vector2.Zero;
        moveAxis.X = Input.GetAxis("DirectionDown", "DirectionUp");
        moveAxis.Y = Input.GetAxis("DirectionLeft", "DirectionRight");

        float elevateAxis = Input.GetAxis("ShiftKey", "SpaceKey");
        float rotateAxis = Input.GetAxis("QKey", "EKey");

        if (Input.IsActionJustPressed("InteractionMouseLeft"))
            Interact1();

        if (Input.IsActionJustPressed("InteractionMouseRight"))
            Interact2();

        if (moveAxis != Vector2.Zero)
            Move(moveAxis);

        if (elevateAxis != 0)
            Elevate(elevateAxis);

        if (rotateAxis != 0)
            Rotate(rotateAxis);

        if (Input.IsActionJustPressed("SpaceKey"))
            SpaceKey();

        if (Input.IsActionJustPressed("ShiftKey"))
            SpaceKey();

        if (Input.IsActionJustPressed("ControlKey"))
            ControlKey();

        if (Input.IsActionJustPressed("AltKey"))
            AltKey();

        if (Input.IsActionJustPressed("Cancel"))
            Cancel();

        if (Input.IsActionJustPressed("OptionTop1"))
            OptionTop1();

        if (Input.IsActionJustPressed("OptionTop2"))
            OptionTop2();

        if (Input.IsActionJustPressed("OptionTop3"))
            OptionTop3();

        if (Input.IsActionJustPressed("OptionTop4"))
            OptionTop4();

        if (Input.IsActionJustPressed("OptionTop5"))
            OptionTop5();
    }

    public virtual void Interact1()
    {
        ActiveInputState.Interact1();
    }

    public virtual void Interact2()
    {
        ActiveInputState.Interact2();
    }

    public virtual void Move(Vector2 axis)
    {
        ActiveInputState.Move(axis);
    }

    public virtual void Elevate(float axis)
    {
        ActiveInputState.Elevate(axis);
    }

    public virtual void Rotate(float axis)
    {
        ActiveInputState.Rotate(axis);
    }

    public virtual void SpaceKey()
    {
        ActiveInputState.SpaceKey();
    }

    public virtual void ShiftKey()
    {
        ActiveInputState.ShiftKey();
    }

    public virtual void ControlKey()
    {
        ActiveInputState.ControlKey();
    }

    public virtual void AltKey()
    {
        ActiveInputState.AltKey();
    }

    public virtual void Cancel()
    {
        ActiveInputState.Cancel();
    }

    public void OptionTop1()
    {
        ActiveInputState.OptionTop1();
    }

    public void OptionTop2()
    {
        ActiveInputState.OptionTop2();
    }

    public void OptionTop3()
    {
        ActiveInputState.OptionTop3();
    }

    public void OptionTop4()
    {
        ActiveInputState.OptionTop4();
    }

    public void OptionTop5()
    {
        ActiveInputState.OptionTop5();
    }
}
using Godot;
using System;
using MovementTestUtilities;

public partial class HF_PlayerMovementManager : CharacterBody3D
{
    [Export]
    private float Speed;

    [Export]
    private AnimationTree m_AnimationTree;

    public override void _Ready()
	{
        GameActions.Move += Move;
	}

    public void Move(Vector2 axis)
    {
        Vector3 axis3D = new Vector3();

        axis3D.X = axis.X * -Speed;
        axis3D.Z = axis.Y * -Speed;

        MoveAndCollide(axis3D);
    }

    public override void _ExitTree()
    {
        GameActions.Move -= Move;
    }
}

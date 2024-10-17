using Godot;
using System;
using MovementTestUtilities;

public partial class PlayerMovementManager : RigidBody3D
{
    [Export]
    private float Speed;

	private RigidBody3D m_RigidBody;

    public override void _Ready()
	{
        GameActions.Move += Move;
	}

    public void Move(Vector2 axis)
    {
        Vector3 axis3D = new Vector3();

        axis3D.X = axis.X * -Speed;
        axis3D.Z = axis.Y * -Speed;

        ApplyCentralForce(axis3D);
    }

    public override void _ExitTree()
    {
        GameActions.Move -= Move;
    }
}

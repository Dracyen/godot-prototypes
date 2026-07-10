using Godot;
using LaserCutterUtilities;
using System.Diagnostics;

public partial class FloatingMovementManager : RigidBody3D
{
    [Export]
    private float Speed = 10;

    private RigidBody3D rigidBody;

    public override void _Ready()
    {
        rigidBody = this;
        GameActions.Move += Move;
        GameActions.Elevate += Elevate;
        GameActions.Rotate += Rotate;
    }

    public void Move(Vector2 axis)
    {
        Vector3 axis3D = new Vector3();

        axis3D.X = axis.Y * Speed;
        axis3D.Z = axis.X * -Speed;

        Debug.WriteLine(GlobalTransform.Basis.Y);

        ApplyCentralForce(axis3D);
    }

    public void Elevate(float axis)
    {
        Vector3 axis3D = new Vector3();

        axis3D.Y = axis * Speed;

        ApplyCentralForce(axis3D);
    }

    public void Rotate(float axis)
    {
        Vector3 axis3D = new Vector3();

        axis3D.Z = axis * -Speed;

        //axis3D *= GlobalTransform.Basis.Y;

        ApplyTorque(axis3D);
    }

    public override void _ExitTree()
    {
        GameActions.Move -= Move;
        GameActions.Elevate -= Elevate;
        GameActions.Rotate -= Rotate;
    }
}

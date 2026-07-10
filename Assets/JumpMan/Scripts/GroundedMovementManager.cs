using Godot;
using HordeFighterUtilities;

public partial class GroundedMovementManager : CharacterBody3D
{
    [Export]
    private float Speed;
    [Export]
    private float Friction;

    private Node3D _node;
    private AnimationPlayer animationPlayer;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    public Vector3 axis3D = Vector3.Zero;
    public Vector3 velocity;
    public bool isMoving = false;

    public override void _Ready()
    {
        _node = (Node3D)this;
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        GameActions.Move += Move;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        HandleFriction();

        if(IsOnFloor())
            HandleAnimation();

        isMoving = false;
        axis3D = Vector3.Zero;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        HandleGravity(delta);
        MoveAndSlide();
    }

    public void Move(Vector2 axis)
    {
        isMoving = true;

        axis3D.X = axis.X * -Speed * (float)GetProcessDeltaTime();
        axis3D.Z = axis.Y * -Speed * (float)GetProcessDeltaTime();

        Velocity += axis3D;
    }

    private void HandleGravity(double delta)
    {
        if (!IsOnFloor())
        {
            velocity = Velocity;

            velocity.Y -= gravity * (float)delta;

            Velocity = velocity;
        }
    }

    private void HandleFriction()
    {
        Vector3 friction = -Velocity / Friction;

        Velocity += friction;
    }

    private void HandleAnimation()
    {
        Vector3 targetDirection = Velocity.Normalized();
        Vector3 currentForward = GlobalTransform.Basis.Z;
        float angle = currentForward.SignedAngleTo(targetDirection, Vector3.Up);

        Basis targetBasis = Basis.Rotated(Vector3.Up, angle);

        Transform = new Transform3D(targetBasis, GlobalTransform.Origin);

        if (!isMoving)
        {
            if (animationPlayer.CurrentAnimation != "idle_animation")
                animationPlayer.Play("idle_animation");
        }
        else
        {
            if (animationPlayer.CurrentAnimation != "walk_animation")
                animationPlayer.Play("walk_animation");
        }
    }

    public override void _ExitTree()
    {
        GameActions.Move -= Move;
    }
}
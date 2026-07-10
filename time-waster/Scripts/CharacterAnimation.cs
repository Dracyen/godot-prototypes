using Godot;
using Microsoft.VisualBasic;
using System;

public partial class CharacterAnimation : AnimatedSprite3D
{
    /// <summary>
    /// Character Animation State
    /// </summary>
    private int direction = 0;
    private AnimState state = AnimState.Idle;
    private string[] idleAnimation =
    {
        "idle_down",
        "idle_down_right",
        "idle_right",
        "idle_up_right",
        "idle_up",
        "idle_up_left",
        "idle_left",
        "idle_down_left"
    };
    private string[] walkAnimation =
    {
        "walk_down",
        "walk_down_right",
        "walk_right",
        "walk_up_right",
        "walk_up",
        "walk_up_left",
        "walk_left",
        "walk_down_left"
    };
    private string[] runAnimation =
    {
        "run_down",
        "run_down_right",
        "run_right",
        "run_up_right",
        "run_up",
        "run_up_left",
        "run_left",
        "run_down_left"
    };
    private Vector2[] directionNormalized =
    {
        new Vector2(0, 1), 					// South
		new Vector2(-0.7071f, 0.7071f), 	// SouthWest
		new Vector2(-1, 0), 				// West
		new Vector2(-0.7071f, -0.7071f), 	// NorthWest
		new Vector2(0, -1), 				// North
		new Vector2(0.7071f, -0.7071f), 	// NorthEast
		new Vector2(1, 0), 					// East
		new Vector2(0.7071f, 0.7071f) 		// SouthEast
	};

    public void UpdateAnimation(Vector3 direction, float cameraYaw = 0)
    {
        Vector2 characterDirection = new Vector2(direction.X, direction.Z);

        float angle = Mathf.Atan2(characterDirection.X, characterDirection.Y) - cameraYaw;

        int directionIndex = DirectionIndexFromVector(angle);

        SetDirection(directionIndex);

        SetAnimation(directionIndex);
    }

    public void UpdateAnimationState(AnimState newState)
    {
        if (state == newState)
            return;

        state = newState;
        SetAnimation(direction);
    }

    private int DirectionIndexFromVector(float angle)
    {
        int directionIndex = Mathf.RoundToInt(angle / (Mathf.Pi / 4)) % 8;

        if (directionIndex < 0)
            directionIndex += 8;

        return directionIndex;
    }

    private void SetDirection(int directionIndex)
    {
        direction = directionIndex;
    }

    private void SetAnimation(int directionIndex)
    {
        string[] animation;

        switch (state)
        {
            case AnimState.Idle:
                animation = idleAnimation;
                break;
            case AnimState.Walk:
                animation = walkAnimation;
                break;
            case AnimState.Run:
                animation = runAnimation;
                break;
            default:
                animation = idleAnimation;
                break;
        }

        GD.Print($"directionIndex: {directionIndex}, animation: {animation[directionIndex]}");

        int lastFrame = Frame;
        float lastProgress = FrameProgress;
        Play(animation[directionIndex]);
        SetFrameAndProgress(lastFrame, lastProgress);
    }
}

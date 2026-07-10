using Godot;
using Godot.Collections;

public partial class CharacterController : CharacterBody3D
{
	[ExportCategory("References")]
	[Export] private Camera3D camera;
	[Export] private CharacterAnimation animator;
	[Export] private NavigationAgent3D navigator;
	[Export] private Node3D animatorAnchor;

	[ExportCategory("Movement Settings")]
	[Export] private float character_speed = 5.0f;

	private Vector3 lastDirection = Vector3.Zero;
	private bool isInteracting = false;
	private bool canCancelInteraction = false;

	public override void _Process(double delta)
	{
		FaceCamera();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (navigator.IsNavigationFinished())
		{
			Velocity = Vector3.Zero;
			animator.UpdateAnimationState(AnimState.Idle);
			return;
		}

		Vector3 nextPath = navigator.GetNextPathPosition();
		Vector3 direction = (nextPath - GlobalPosition).Normalized();
		lastDirection = direction;

		UpdateAnimation();

		Velocity = direction * character_speed;
		MoveAndSlide();
	}

	public void SetTarget(Vector3 targetPosition)
	{
		navigator.TargetPosition = targetPosition;

		Vector3 nextPath = navigator.GetNextPathPosition();

		lastDirection = (nextPath - GlobalPosition).Normalized();

		animator.UpdateAnimationState(AnimState.Walk);

		UpdateAnimation();
	}

	public void UpdateAnimation()
	{
		animator.UpdateAnimation(lastDirection, camera.Rotation.Y);
	}

	public void Interact(Dictionary result)
	{
		if (isInteracting && !canCancelInteraction)
			return;

		if (!result.ContainsKey("collider"))
			return;

		Node3D collider = (Node3D)result["collider"];

		if (collider is IInteractor interactor)
		{
			isInteracting = true;
			canCancelInteraction = interactor.CanBeCanceled;
			interactor.Interact(this);
			return;
		}

		SetTarget((Vector3)result["position"]);
	}

	private void FaceCamera()
	{
		if (camera != null)
			animatorAnchor.Rotation = camera.Rotation;
	}
}
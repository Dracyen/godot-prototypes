using Godot;
using Godot.Collections;

public partial class CameraController : Camera3D
{
	[ExportCategory("Camera Settings")]
	[Export] float camera_speed = 20.0f;
	[Export] float camera_zoom_speed = 20.0f;
	[Export] float camera_zoom_min = 10.0f;
	[Export] float camera_zoom_max = 50.0f;
	[Export] CharacterController character = null;

	[ExportCategory("Edge scrolling")]
	[Export] float edge_scroll_margin = 20.0f;
	[Export] float edge_scroll_speed = 15.0f;

	[ExportCategory("Rotation")]
	[Export] float yaw_sensitivity = 0.50f;
	[Export] float pitch_sensitivity = 0.18f;
	[Export] float max_step_deg = 3.0f;
	[Export] float pitch_min_deg = 10.0f;
	[Export] float pitch_max_deg = 80.0f;
	[Export] bool capture_mouse_on_mmb = false;


	Vector3 orbit_center = Vector3.Zero;
	float orbit_distance = 25.0f;
	float current_height = 20.0f;
	float orbit_radius = 20.0f;

	bool _is_mmb_rotating = false;
	float _yaw = 0.0f;
	float _pitch = 0.8f;
	int _lastDirectionIndex = -1;

	public override void _Ready()
	{
		float pmin = Mathf.DegToRad(pitch_min_deg);
		float pmax = Mathf.DegToRad(pitch_max_deg);
		_pitch = Mathf.Clamp(_pitch, pmin, pmax);
		_update_camera_position();
	}

	public override void _Process(double delta)
	{
		Vector3 forward = -GlobalTransform.Basis.Z;
		float angle = Mathf.Atan2(forward.X, forward.Z);

		int directionIndex = Mathf.RoundToInt(angle / (Mathf.Pi / 4)) % 8;
		if (directionIndex < 0)
			directionIndex += 8;

		if (directionIndex != _lastDirectionIndex)
		{
			GD.Print($"Direction changed: {directionIndex}");
			_lastDirectionIndex = directionIndex;
			OnDirectionChanged();
		}

		Vector3 movement = Vector3.Zero;

		if (Input.IsActionPressed("ui_right"))
			movement.X += 1;
		if (Input.IsActionPressed("ui_left"))
			movement.X -= 1;
		if (Input.IsActionPressed("ui_up"))
			movement.Z -= 1;
		if (Input.IsActionPressed("ui_down"))
			movement.Z += 1;

		Vector2 mouse_pos = GetViewport().GetMousePosition();
		Vector2 viewport_size = GetViewport().GetVisibleRect().Size;

		if(mouse_pos.X >= 0 && mouse_pos.X < viewport_size.X)
			return;

		if (mouse_pos.Y >= 0 && mouse_pos.Y < viewport_size.Y)
			return;

		if (mouse_pos.X < edge_scroll_margin)
			movement.X -= 1;
		else if (mouse_pos.X > viewport_size.X - edge_scroll_margin)
			movement.X += 1;

		if (mouse_pos.Y < edge_scroll_margin)
			movement.Z -= 1;
		else if (mouse_pos.Y > viewport_size.Y - edge_scroll_margin && mouse_pos.Y <= viewport_size.Y)
			movement.Z += 1;

		if (movement.Length() > 0.0f)
		{
			movement = movement.Normalized().Rotated(Vector3.Up, _yaw);
			orbit_center += movement * camera_speed * (float)delta;
			_update_camera_position();
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
		{
			PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
			Vector2 mousePos = GetViewport().GetMousePosition();

			Vector3 rayOrigin = ProjectRayOrigin(mousePos);
			Vector3 rayEnd = rayOrigin + ProjectRayNormal(mousePos) * 1000f;

			PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
			query.Exclude = new Array<Rid> { character.GetRid() };
			Dictionary result = spaceState.IntersectRay(query);

			if (result.Count > 0)
			{
				GD.Print(result);
				character.Interact(result);
			}
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseWheelEvent)
		{
			if (mouseWheelEvent.Pressed && mouseWheelEvent.ButtonIndex == MouseButton.WheelUp)
			{
				orbit_distance = Mathf.Max(camera_zoom_min, orbit_distance - camera_zoom_speed * (float)GetProcessDeltaTime());
				_update_camera_position();
			}
			else if (mouseWheelEvent.Pressed && mouseWheelEvent.ButtonIndex == MouseButton.WheelDown)
			{
				orbit_distance = Mathf.Min(camera_zoom_max, orbit_distance + camera_zoom_speed * (float)GetProcessDeltaTime());
				_update_camera_position();
			}

			if (mouseWheelEvent.ButtonIndex == MouseButton.Middle)
			{
				_is_mmb_rotating = mouseWheelEvent.Pressed;
				if (capture_mouse_on_mmb)
					Input.MouseMode = mouseWheelEvent.IsPressed() ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible;
			}
		}
		
		if (@event is InputEventMouseMotion mouseMotionEvent && _is_mmb_rotating)
		{
			Vector2 vp = GetViewport().GetVisibleRect().Size;
			float vmin = Mathf.Min(vp.X, vp.Y);
			float dt = (float)GetProcessDeltaTime();
			float sixty_fps = 60.0f * dt;

			float dx = mouseMotionEvent.Relative.X / vmin * yaw_sensitivity   * Mathf.Tau * sixty_fps;
			float dy = mouseMotionEvent.Relative.Y / vmin * pitch_sensitivity * Mathf.Tau * sixty_fps;

			float max_step = Mathf.DegToRad(max_step_deg);
			dx = Mathf.Clamp(dx, -max_step, max_step);
			dy = Mathf.Clamp(dy, -max_step, max_step);

			_yaw   -= dx;
			_pitch += dy;

			float pmin = Mathf.DegToRad(pitch_min_deg);
			float pmax = Mathf.DegToRad(pitch_max_deg);
			_pitch = Mathf.Clamp(_pitch, pmin, pmax);

			_update_camera_position();
		}
	}

	private void OnDirectionChanged()
	{
		character.UpdateAnimation();
	}

	public void _update_camera_position()
	{
		Vector3 dir = new Vector3(
			Mathf.Sin(_yaw) * Mathf.Cos(_pitch),
			Mathf.Sin(_pitch),
			Mathf.Cos(_yaw) * Mathf.Cos(_pitch)
		).Normalized();

		Position = orbit_center + dir * orbit_distance;
		LookAt(orbit_center, Vector3.Up);

		current_height = orbit_distance * Mathf.Sin(_pitch);
		orbit_radius = orbit_distance * Mathf.Cos(_pitch);
	}
}

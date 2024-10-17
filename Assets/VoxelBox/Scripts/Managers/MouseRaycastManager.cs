using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class MouseRaycastManager : Camera3D
{
	[Export]
	float length = 1000;

    public override void _PhysicsProcess(double delta)
    {
        PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;

        Vector2 mousePos = GetViewport().GetMousePosition();

        Vector3 origin = ProjectRayOrigin(mousePos);

        Vector3 end = origin + ProjectRayNormal(mousePos) * length;

        PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(origin, end);

        query.CollideWithAreas = true;

        Dictionary result = spaceState.IntersectRay(query);

        if (result.Count > 0)
        {
            GridManager grid = (GridManager)result["collider"];

            if (grid != null)
            {
                Vector3 pos = (Vector3)result["position"];

                grid.UpdateGridSelection(pos);
            }
        }
    }
}
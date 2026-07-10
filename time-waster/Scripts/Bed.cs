using Godot;
using System;

public partial class Bed : StaticBody3D, IInteractor
{
    [ExportCategory("Interaction Nodes")]
    [Export]public Node3D[] InteractionSpots { get; private set;} 

    [ExportCategory("Interaction Settings")]
    [Export]public bool CanBeCanceled { get ; private set;}

    public void Interact(CharacterController interactor)
    {
        Node3D closestSpot = InteractionSpots[0];

        foreach (Node3D spot in InteractionSpots)
        {
            if (closestSpot == null || spot.GlobalPosition.DistanceTo(interactor.GlobalPosition) < closestSpot.GlobalPosition.DistanceTo(interactor.GlobalPosition))
                closestSpot = spot;
        }

        interactor.SetTarget(closestSpot.GlobalPosition);
    }
}
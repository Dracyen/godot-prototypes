using Godot;
using System;

public partial interface IInteractor
{
    public Node3D[] InteractionSpots { get; }
    public bool CanBeCanceled { get; }
    public void Interact(CharacterController interactor);
}

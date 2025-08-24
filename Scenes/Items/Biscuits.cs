using Godot;
using System;

public partial class Biscuits : Node3D
{
	public bool _CanActivate = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area3D pickupArea = GetNode<Area3D>("PickupArea");
		pickupArea.BodyEntered += OnBodyEntered;
		pickupArea.BodyExited += OnBodyExited;

		GD.Print("Biscuits Ready!");
	}

	public void Initialize(Player player)
	{
		player.ActivateObject += OnActivateObject;
	}

	public void OnBodyEntered(Node3D body)
	{
		_CanActivate = true;
        GD.Print("Entered Biscuits range!");
	}

    public void OnBodyExited(Node3D body)
    {
		_CanActivate = false;
        GD.Print("Left Biscuits range!");
    }

	public void OnActivateObject()
	{
		if (!_CanActivate) {
			return;
		}

		// TOOD(nemjit001): Signal player that we have collected the biscuits and update player state
		GD.Print("Biscuits Collected!");
		QueueFree();
	}
}

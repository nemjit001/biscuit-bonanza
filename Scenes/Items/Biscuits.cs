using Godot;
using System;

public partial class Biscuits : Node3D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area3D pickupArea = GetNode<Area3D>("PickupArea");
		pickupArea.BodyEntered += OnBodyEntered;
		pickupArea.BodyExited += OnBodyExited;

		GD.Print("Biscuits Ready!");
	}

	public void OnBodyEntered(Node3D body)
	{
		GD.Print("Entered Biscuits range!");
	}

    public void OnBodyExited(Node3D body)
    {
        GD.Print("Left Biscuits range!");
    }

	public void OnActivate()
	{
		// TOOD(nemjit001): Signal game manager that we have collected the biscuits!
		QueueFree();
	}
}

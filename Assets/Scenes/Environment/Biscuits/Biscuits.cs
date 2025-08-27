using Godot;
using System;

public partial class Biscuits : Node3D
{
	const string LOUIE_GROUP = "louie";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area3D pickup = GetNode<Area3D>("PickupArea");
		pickup.BodyEntered += OnBodyEntered;
		pickup.BodyExited += OnBodyExited;

		GD.Print("Biscuits Ready!");
	}

	public void OnBodyEntered(Node3D node)
	{
		if (node.IsInGroup(LOUIE_GROUP))
		{
			PlayerBody body = (PlayerBody)node;
			body.Player.ActivateObject += OnActivateObject;
		}
	}

    public void OnBodyExited(Node3D node)
    {
        if (node.IsInGroup(LOUIE_GROUP))
        {
            PlayerBody body = (PlayerBody)node;
            body.Player.ActivateObject -= OnActivateObject;
        }
    }

    public void OnActivateObject(Player player)
    {
		if (player.HasBiscuits()) {
			return;
		}

        player.CollectBiscuits(this);
        GD.Print($"Biscuits Collected!");
		QueueFree();
	}
}

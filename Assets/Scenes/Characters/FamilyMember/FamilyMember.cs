using Godot;
using System;

public partial class FamilyMember : Node3D
{
	const string LOUIE_GROUP = "louie";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area3D captureArea = GetNode<Area3D>("CaptureArea");
		captureArea.BodyEntered += OnBodyEntered;

		GD.Print("FamilyMember Ready!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//
	}

	public void OnBodyEntered(Node3D node)
	{
		GD.Print("Did we catch him?");
		if (node.IsInGroup(LOUIE_GROUP)) {
			Player player = (Player)node;
			if (player.HasBiscuits()) {
				GD.Print("He has the biscuits!");
			}
		}
	}
}

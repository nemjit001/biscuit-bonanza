using Godot;
using System;

public partial class FamilyMember : Node3D
{
	const string LOUIE_GROUP = "louie";

	NavigationAgent3D _NavAgent = null;

	public override void _Ready()
	{
		Area3D captureArea = GetNode<Area3D>("CaptureArea");
		captureArea.BodyEntered += OnBodyEntered;

		_NavAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		GD.Print("FamilyMember Ready!");
	}

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
				// TODO(nemjit001): Save capture and restart level
			}
		}
	}
}

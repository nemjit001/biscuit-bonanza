using Godot;
using System;

public partial class BiscuitManager : Node3D
{
	[Export]
	PackedScene BiscuitsScene = null;

	public override void _Ready()
	{
		// Spawn biscuits at random location
		Player player = GetNode<Player>("../Player");
        Biscuits biscuits = BiscuitsScene.Instantiate<Biscuits>();
		biscuits.Initialize(player);
		AddChild(biscuits);
		
		GD.Print("Biscuit Manager Ready!");
	}
}

using Godot;
using System;

public partial class DogHouse : Node3D
{
	const string BISCUITS_GROUP = "biscuits";

	public override void _Ready()
	{
		Area3D dropArea = GetNode<Area3D>("DropArea");
		dropArea.AreaEntered += OnAreaEntered;

		GD.Print("DogHouse Ready!");
	}

	public void OnAreaEntered(Area3D area)
	{
		GD.Print("Something dropped at DogHouse!");
		if (area.IsInGroup(BISCUITS_GROUP)) {
			GD.Print("Biscuits Delivered!");
			ScoreManager.Instance.Score.BiscuitsCollected += 1;
			// TODO(nemjit001): Restart level (with higher difficulty?)
		}
	}
}

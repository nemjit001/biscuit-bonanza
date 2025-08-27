using Godot;
using System;

public partial class DogHouse : Node3D
{
	const string BISCUITS_GROUP = "biscuits";

	private int _TargetBiscuitCount = 0;
	private int _DeliveredCount = 0;

	public override void _Ready()
	{
		Area3D dropArea = GetNode<Area3D>("DropArea");
		dropArea.AreaEntered += OnAreaEntered;

		_TargetBiscuitCount = GetTree().GetNodesInGroup(BISCUITS_GROUP).Count;
        GD.Print("DogHouse Ready!");
	}

    public override void _Process(double delta)
    {
		if (_DeliveredCount == _TargetBiscuitCount)
		{
			// All biscuits collected!
			// Save score and exit game
			ScoreManager.Instance.Save();
			GameManager.Instance.LoadScene(GameManager.Instance.Levels.ScoreScenePath);
		}
    }

	public void OnAreaEntered(Area3D area)
	{
		GD.Print("Something dropped at DogHouse!");
		if (area.IsInGroup(BISCUITS_GROUP)) {
			GD.Print("Biscuits Delivered!");

            // Delete delivered biscuits and register score
            ScoreManager.Instance.Score.BiscuitsCollected += 1;
            Node3D biscuits = area.GetOwner<Node3D>();
			biscuits.QueueFree();

			_DeliveredCount += 1;
        }
	}
}

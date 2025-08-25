using Godot;
using System;

public partial class PauseMenu : Control
{
    const string OPEN_MENU = "open_menu";

	public override void _Ready()
	{
		Hide();
		GD.Print("PauseMenu Ready!");
	}

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(OPEN_MENU)) {
			TogglePause();
		}
    }

	public void TogglePause()
	{
		SceneTree tree = GetTree();
		tree.Paused = !tree.Paused;
		if (tree.Paused) {
			Show();
		}
		else {
			Hide();
		}
	}

	public void OnClosePressed()
	{
		TogglePause();
	}

	public void OnQuitPressed()
	{
		GameManager.Instance.QuitGame();
	}
}

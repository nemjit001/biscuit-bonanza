using Godot;
using System;

public partial class PauseMenu : Control
{
    const string OPEN_MENU = "open_menu";

	MarginContainer _PauseMenuContainer = null;

    public override void _Ready()
	{
		_PauseMenuContainer = GetNode<MarginContainer>("PauseMenuContainer");
		_PauseMenuContainer.Hide();

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
            _PauseMenuContainer.Show();
		}
		else {
            _PauseMenuContainer.Hide();
		}
	}

	public void OnClosePressed()
	{
		TogglePause();
	}

    public void OnQuitToMenuPressed()
    {
		SceneTree tree = GetTree();
		tree.Paused = false;

		GameManager.Instance.LoadScene("res://Assets/Scenes/main.tscn");
    }

    public void OnQuitPressed()
	{
        SceneTree tree = GetTree();
        tree.Paused = false;

        GameManager.Instance.QuitGame();
	}
}

using Godot;
using System;

public partial class PauseMenu : Control
{
    const string OPEN_MENU = "open_menu";

    Control _PauseMenuContainer = null;

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

	void TogglePause()
	{
		SceneTree tree = GetTree();
		if (tree.Paused) {
            UnPause();
		}
		else {
            Pause();
		}
	}

    void Pause()
    {
        SceneTree tree = GetTree();
        tree.Paused = true;

        Button closeButton = GetNode<Button>("MarginContainer/VBoxContainer/CloseButton");
        closeButton.GrabFocus();

        Show();
    }

    void UnPause()
    {
        SceneTree tree = GetTree();
        tree.Paused = false;

        Hide();
    }

    public void OnClosePressed()
	{
        UnPause();
	}

    public void OnQuitToMenuPressed()
    {
        UnPause();
		GameManager.Instance.LoadScene(GameManager.Instance.Levels.MainMenuScenePath);
    }

    public void OnQuitPressed()
	{
        UnPause();
        GameManager.Instance.QuitGame();
	}
}

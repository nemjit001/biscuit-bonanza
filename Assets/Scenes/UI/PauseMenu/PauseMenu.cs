using Godot;
using System;

public partial class PauseMenu : Control
{
    const string OPEN_MENU = "open_menu";

    [Export(PropertyHint.File, "*.tscn,*.scn")]
    public string MainMenuScenePath;

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

        _PauseMenuContainer.Show();
    }

    void UnPause()
    {
        SceneTree tree = GetTree();
        tree.Paused = false;

        _PauseMenuContainer.Hide();
    }

    public void OnClosePressed()
	{
        UnPause();
	}

    public void OnQuitToMenuPressed()
    {
        UnPause();
		GameManager.Instance.LoadScene(MainMenuScenePath);
    }

    public void OnQuitPressed()
	{
        UnPause();
        GameManager.Instance.QuitGame();
	}
}

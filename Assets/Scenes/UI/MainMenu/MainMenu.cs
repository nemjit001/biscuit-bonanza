using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        GetNode<Button>("MarginContainer/VBoxContainer/PlayButton").GrabFocus();
    }

    public void OnPlayPressed()
	{
		GameManager.Instance.LoadScene(GameManager.Instance.Levels.GameScenePath);
	}

	public void OnQuitPressed()
	{
		GameManager.Instance.QuitGame();
	}
}

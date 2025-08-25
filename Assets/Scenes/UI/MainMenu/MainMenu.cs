using Godot;
using System;

public partial class MainMenu : Control
{
	public void OnPlayPressed()
	{
		GameManager.Instance.LoadScene("res://Assets/Scenes/demo_scene.tscn");
	}

	public void OnQuitPressed()
	{
		GameManager.Instance.QuitGame();
	}
}

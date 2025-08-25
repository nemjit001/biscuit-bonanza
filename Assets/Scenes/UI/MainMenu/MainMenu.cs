using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] public PackedScene GameScene = null;

	public void OnPlayPressed()
	{
		GameManager.Instance.LoadScene(GameScene.ResourcePath);
	}

	public void OnQuitPressed()
	{
		GameManager.Instance.QuitGame();
	}
}

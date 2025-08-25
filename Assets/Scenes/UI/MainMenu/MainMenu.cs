using Godot;
using System;

public partial class MainMenu : Control
{
    [Export(PropertyHint.File, "*.tscn,*.scn")]
    public string GameScenePath;

    public void OnPlayPressed()
	{
		GameManager.Instance.LoadScene(GameScenePath);
	}

	public void OnQuitPressed()
	{
		GameManager.Instance.QuitGame();
	}
}

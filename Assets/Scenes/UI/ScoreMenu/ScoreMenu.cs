using Godot;
using System;

public partial class ScoreMenu : Control
{
    public override void _Ready()
    {
        Label timesCaught = GetNode<Label>("MarginContainer/VBoxContainer/HScoreContainer/ScoreLabels/TimesCaughtScore");
        Label timesEscaped = GetNode<Label>("MarginContainer/VBoxContainer/HScoreContainer/ScoreLabels/TimesEscapedScore");
        Label biscuitsCollected = GetNode<Label>("MarginContainer/VBoxContainer/HScoreContainer/ScoreLabels/BiscuitsCollectedScore");

        timesCaught.Text = ScoreManager.Instance.Score.TimesCaught.ToString();
        timesEscaped.Text = "0";
        biscuitsCollected.Text = ScoreManager.Instance.Score.BiscuitsCollected.ToString();
    }

    public void OnQuitToMenuPressed()
    {
        GameManager.Instance.LoadScene(GameManager.Instance.Levels.MainMenuScenePath);
    }
}

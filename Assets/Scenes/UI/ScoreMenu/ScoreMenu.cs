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
        timesEscaped.Text = ScoreManager.Instance.Score.TimesEscaped.ToString();
        biscuitsCollected.Text = ScoreManager.Instance.Score.BiscuitsCollected.ToString();
    }

    public void OnQuitToMenuPressed()
    {
        // Save score and go back to menu game
        ScoreManager.Instance.Save();
        ScoreManager.Instance.ResetScore();
        GameManager.Instance.LoadScene(GameManager.Instance.Levels.MainMenuScenePath);
    }
}

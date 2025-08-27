using Godot;
using System;

public partial class ScoreMenu : Control
{
    public override void _Ready()
    {
        Label timesCaught = GetNode<Label>("ColorRect/MarginContainer/VBoxContainer/HScoreContainer/ScoreLabels/TimesCaughtScore");
        Label timesEscaped = GetNode<Label>("ColorRect/MarginContainer/VBoxContainer/HScoreContainer/ScoreLabels/TimesEscapedScore");
        Label biscuitsCollected = GetNode<Label>("ColorRect/MarginContainer/VBoxContainer/HScoreContainer/ScoreLabels/BiscuitsCollectedScore");

        timesCaught.Text = ScoreManager.Instance.PrevScore.TimesCaught.ToString();
        timesEscaped.Text = "0";
        biscuitsCollected.Text = ScoreManager.Instance.PrevScore.BiscuitsCollected.ToString();
    }

    public void OnQuitToMenuPressed()
    {
        GameManager.Instance.LoadScene(GameManager.Instance.Levels.GameScenePath);
    }
}

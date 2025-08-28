using Godot;
using System;

public partial class ScoreManager : Node
{
    const string SAVE_GAME_FILE = "user://game.save";

    public static ScoreManager Instance { get; private set; } = null;

    public PlayerScore Score { get; set; }
    public PlayerScore PrevScore { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        ResetScore();

    }

    public void ResetScore()
    {
        Score = new PlayerScore();
        PrevScore = GetPreviousScore();
    }

    public void Save()
    {
        ResourceSaver.Save(Score, SAVE_GAME_FILE);
    }

    private PlayerScore GetPreviousScore()
    {
        if (ResourceLoader.Exists(SAVE_GAME_FILE))
        {
            return GD.Load<PlayerScore>(SAVE_GAME_FILE);
        }
        
        return new PlayerScore();
    }
}

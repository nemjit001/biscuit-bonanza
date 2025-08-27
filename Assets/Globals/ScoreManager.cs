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
        Score = new PlayerScore();
        if (ResourceLoader.Exists(SAVE_GAME_FILE)) {
            PrevScore = GD.Load<PlayerScore>(SAVE_GAME_FILE);
        }
        else {
            PrevScore = new PlayerScore();
        }
    }

    public void Save()
    {
        ResourceSaver.Save(Score, SAVE_GAME_FILE);
    }
}

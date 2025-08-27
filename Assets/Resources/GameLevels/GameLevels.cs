using Godot;
using System;

public partial class GameLevels : Resource
{
    [Export(PropertyHint.File, "*.tscn,*.scn")]
    public string MainMenuScenePath;

    [Export(PropertyHint.File, "*.tscn,*.scn")]
    public string GameScenePath;

    [Export(PropertyHint.File, "*.tscn,*.scn")]
    public string ScoreScenePath;

    public GameLevels() { }
}

using Godot;
using System;

public partial class PlayerScore : Resource
{
    [Export]
    public int TimesSpotted = 0;
    
    [Export]
    public int TimesCaught = 0;
    
    [Export]
    public int BiscuitsCollected = 0;

    public PlayerScore() {}

    public PlayerScore(int timesSpotted, int timesCaught, int biscuitsCollected)
    {
        TimesSpotted = timesSpotted;
        TimesCaught = timesCaught;
        BiscuitsCollected = biscuitsCollected;
    }
}

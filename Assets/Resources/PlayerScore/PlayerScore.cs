using Godot;
using System;

public partial class PlayerScore : Resource
{    
    [Export]
    public int TimesCaught = 0;

    [Export]
    public int TimesEscaped = 0;

    [Export]
    public int BiscuitsCollected = 0;

    public PlayerScore() {}

    public PlayerScore(int timesCaught, int timesEscaped, int biscuitsCollected)
    {
        TimesCaught = timesCaught;
        TimesEscaped = timesEscaped;
        BiscuitsCollected = biscuitsCollected;
    }
}

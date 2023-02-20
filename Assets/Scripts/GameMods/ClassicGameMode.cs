using System;

public class ClassicGameMode : GameMode
{   
    public override void StartMode() 
    {
        _value = _loadedValue;
        _hud.DeactivateReloadButton();
        _hud.DeactivateHighscore();
        _hud.SetScore(_value);
    }

    public override void Restart() { }

    public override void Exit() 
    {
        Clear();
        if (_value > _loadedValue)
        {
            SaveSystem.Save(GetType().ToString(), _value);
        }

        _hud.ActivateReloadButton();
        _hud.ActivateHighscore();
    }

    public override void Poped(PopPeace pop)
    {
        _value++;
        _hud.SetScore(_value);
    }

    public override LeaderboardConfig GetValuesForLeaderboard()
    {
        LeaderboardConfig config = new LeaderboardConfig();
        config.Key = _leaderboardKey;
        config.Value = _value;
        return config;
    }
}

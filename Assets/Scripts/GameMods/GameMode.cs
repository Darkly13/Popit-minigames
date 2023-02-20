using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    [SerializeField] protected string _leaderboardKey;
    [SerializeField] protected string _modeName;
    [SerializeField] protected string _modeDescription;
    [SerializeField] protected AnimationClip _animatedGuide;

    protected List<PopPeace> _pops;
    protected HUD _hud;
    protected GuideScreen _guideScreen;
    protected float _loadedValue;
    protected float _value;
       
    public void Initialize(List<PopPeace> pops, HUD hud, GuideScreen guideScreen) 
    {
        _pops = pops;
        _hud = hud;
        _guideScreen = guideScreen;
        _loadedValue = SaveSystem.TryLoad(GetType().ToString());
    }
    
    public abstract void StartMode();
    public abstract void Restart();
    public abstract void Exit();
    public abstract void Poped(PopPeace pop);
    public abstract LeaderboardConfig GetValuesForLeaderboard();

    protected void Clear()
    {
        foreach (PopPeace pop in _pops)
            if (!pop.IsPoped)
                pop.Pop();
    }
}

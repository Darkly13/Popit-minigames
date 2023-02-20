using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedMode : GameMode
{
    private float _highscore;

    private List<PopPeace> _free;
    private List<PopPeace> _picked;

    private bool _roundStarted = false;
    private Coroutine _coroutine;
    private Coroutine _timer;

    public override void StartMode()
    {
        _highscore = _loadedValue;
        _value = 0;
        _hud.SetHighscore(_highscore);
        _hud.SetScore(_value);
        _hud.DeactivateReloadButton();

        _free = new List<PopPeace>();
        foreach (PopPeace pop in _pops)
            _free.Add(pop);

        Clear();
        ShowGuide();
    }

    public override void Poped(PopPeace pop)
    {
        if (!_roundStarted)
            return;

        if (pop.IsPoped)
        {
            _free.Add(pop);
            _picked.Remove(pop);
            if(_picked.Count<=0)
                WinRound();
        }
        else if (!pop.IsPoped)
        {
            _free.Remove(pop);
            _picked.Add(pop);
        }
    }

    public override void Restart()
    {
        Clear();
        _hud.DeactivateReloadButton();
        _value = 0;
        _hud.SetScore(_value);
        _coroutine = StartCoroutine(GameRound());
    }

    public override void Exit()
    {
        StopAllCoroutines();
        _hud.SetBlockText("");
        _hud.Unblock();     
        Clear();
        _hud.ActivateReloadButton();
        _hud.DeactivateGameOverScreen();
    }

    public override LeaderboardConfig GetValuesForLeaderboard()
    {
        LeaderboardConfig config = new LeaderboardConfig();
        config.Key = _leaderboardKey;
        config.Value = _highscore;
        return config;
    }

    private void ShowGuide()
    {
        GuideConfiguration configuration = new GuideConfiguration();
        configuration.Name = _modeName;
        configuration.Description = _modeDescription;
        configuration.Animation = _animatedGuide;
        _guideScreen.OnHided += GuideHided;
        _guideScreen.Initialize(configuration);
    }

    private void GuideHided()
    {
        _guideScreen.OnHided -= GuideHided;
        _coroutine = StartCoroutine(GameRound());
    }

    private IEnumerator GameRound()
    {
        _roundStarted = false;
        _hud.Block();
        PickRandomTiles();
        _hud.SetBlockText("3");
        yield return new WaitForSeconds(1f);
        _hud.SetBlockText("2");
        yield return new WaitForSeconds(1f);
        _hud.SetBlockText("1");
        yield return new WaitForSeconds(1f);
        _hud.SetBlockText("");
        _hud.Unblock();
        _timer = StartCoroutine(Timer());
        _roundStarted = true;

    }

    private void PickRandomTiles()
    {
        _picked = new List<PopPeace>();
        for (int i = 0; i < 15; i++)
            GetRandomTile();
    }

    private void GetRandomTile()
    {
        int count = _free.Count;
        int index = UnityEngine.Random.Range(0, count);
        _picked.Add(_free[index]);
        _free[index].Pop();
        _free.RemoveAt(index);
    }

    private void WinRound()
    {
        _roundStarted = false;
        StopCoroutine(_timer);
        if (_highscore==0 || _value < _highscore)
        {
            _highscore = _value;
            _hud.SetHighscore(_value);
        }
        if (_loadedValue == 0 || _highscore < _loadedValue)
            SaveSystem.Save(GetType().ToString(), _highscore);
        _hud.ActivateReloadButton();
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _value = (float)Math.Round(_value + 0.1f, 2);
            _hud.SetScore((float)Math.Round(_value, 2));
        }
    }
}

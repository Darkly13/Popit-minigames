using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Speed2Mode : GameMode
{
    private float _highscore;

    private List<PopPeace> _free;
    private List<PopPeace> _temporal;

    private bool _roundStarted = false;
    private float _delay;

    public override void StartMode()
    {
        _highscore = _loadedValue;
        _value = 0;
        _delay = 1f;
        _hud.SetHighscore(_highscore);
        _hud.SetScore(_value);
        _hud.DeactivateReloadButton();

        _free = new List<PopPeace>();
        _temporal = new List<PopPeace>();
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
            _temporal.Remove(pop);
        }
        if (!pop.IsPoped)
        {
            _free.Remove(pop);
            _temporal.Add(pop);
        }
    }

    public override void Restart()
    {
        Clear();
        _hud.DeactivateGameOverScreen();
        _hud.DeactivateReloadButton();
        _value = 0;
        _delay = 1f;
        _hud.SetScore(_value);
        StartCoroutine(GameRound());
    }

    public override void Exit()
    {
        StopAllCoroutines();
        _hud.SetBlockText("");
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
        StartCoroutine(GameRound());
    }

    private IEnumerator GameRound()
    {
        _roundStarted = false;
        StartCoroutine(Timer());
        StartCoroutine(Delay());
        _roundStarted = true;
        while (true)
        {
            PickRandomTile();
            if (_free.Count == 0)
                GameOver();
            yield return new WaitForSeconds(_delay);         
        }
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_delay > 0.2f)
                _delay -= 0.02f;
        }
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

    private void PickRandomTile()
    {
        int count = _free.Count;
        int index = UnityEngine.Random.Range(0, count);
        _free[index].Pop();
    }

    private void GameOver()
    {
        _roundStarted = false;
        StopAllCoroutines();
        _hud.ActivateGameOverScreen();
        if (_value > _highscore)
        {
            _highscore = _value;
            _hud.SetHighscore(_value);
        }
        if (_highscore > _loadedValue)
            SaveSystem.Save(GetType().ToString(), _highscore);
        foreach (PopPeace pop in _temporal)
        {
            _free.Add(pop);
        }
        _hud.ActivateReloadButton();
    }
}

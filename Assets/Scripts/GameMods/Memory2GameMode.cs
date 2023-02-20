using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory2GameMode : GameMode
{
    private float _highscore;

    private List<PopPeace> _free;
    private List<PopPeace> _temporal;

    private float _delay = 1f;
    private bool _roundStarted = false;
    private Coroutine _coroutine;

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

        ShowGuide();
    }

    public override void Poped(PopPeace pop)
    {
        if (!_roundStarted)
            return;

        if (pop!=_temporal[0])
        {
            GameOver();
        }
        else
        {
            _temporal.Remove(pop);
            Debug.Log(_temporal.Count);
            _free.Add(pop);
            if (_temporal.Count <= 0)
                WinRound();
        }
    }

    public override void Restart()
    {
        Clear();
        _hud.DeactivateGameOverScreen();
        _hud.DeactivateReloadButton();
        _value = 0;
        _hud.SetScore(_value);
        _coroutine=StartCoroutine(GameRound());
    }

    public override void Exit()
    {
        StopCoroutine(_coroutine);
        Clear();
        if (_highscore > _loadedValue)
            SaveSystem.Save(GetType().ToString(), _highscore);
        _hud.ActivateReloadButton();
        _hud.DeactivateGameOverScreen();
        _hud.Unblock();
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
        _coroutine=StartCoroutine(GameRound());
    }

    private IEnumerator GameRound()
    {
        _roundStarted = false;
        _hud.Block();
        yield return new WaitForSeconds(_delay);
        Clear();
        yield return new WaitForSeconds(_delay);     
        PickRandomTiles();

        int count = _temporal.Count;
        for (int i = 0; i < count; i++)
        {
            _temporal[i].Pop();
            yield return new WaitForSeconds(_delay);
            _temporal[i].Pop();
        }

        _hud.Unblock();
        _roundStarted = true;
    }

    private void PickRandomTiles()
    {
        _temporal = new List<PopPeace>();
        float countOfIteration = (_value + 1) > 15 ? 15 : (_value + 1);
        for (int i = 0; i < countOfIteration; i++)
            GetRandomTile();
    }

    private void GetRandomTile()
    {
        int count = _free.Count;
        int index = Random.Range(0, count);
        _temporal.Add(_free[index]);
        _free.RemoveAt(index);
    }

    private void GameOver()
    {
        _hud.ActivateGameOverScreen();
        if (_highscore > _loadedValue)
            SaveSystem.Save(GetType().ToString(), _highscore);
        foreach (PopPeace pop in _temporal)
        {
            _free.Add(pop);
        }
        _hud.ActivateReloadButton();
    }

    private void WinRound()
    {
        _value++;
        _hud.SetScore(_value);
        if (_value > _highscore)
        {
            _highscore = _value;
            _hud.SetHighscore(_value);
        }
        _coroutine=StartCoroutine(GameRound());
    }
}

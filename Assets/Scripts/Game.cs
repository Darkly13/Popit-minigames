using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Popit _popit;
    [SerializeField] private HUD _hud;
    [SerializeField] private GuideScreen _guideScreen;
    [SerializeField] private SoundController _soundController;
    [SerializeField] private LeaderboardSystem _leaderboardSystem;

    private bool _isInMenu;
    private GameMode _gameMode;
    private List<PopPeace> _pops;

    public void Awake()
    {
        if (_popit == null)
            throw new NullReferenceException();
        if (_hud == null)
            throw new NullReferenceException();
        if (_guideScreen == null)
            throw new NullReferenceException();
        if (_soundController == null)
            throw new NullReferenceException();
        if (_leaderboardSystem == null)
            throw new NullReferenceException();
    }

    public void Start()
    {
        _pops = _popit.CreateField();
        foreach(PopPeace pop in _pops)
            pop.OnPoped += Poped;
    }

    public void StartGame(GameMode gameMode)
    {
        _gameMode = gameMode;
        _gameMode.Initialize(_pops, _hud, _guideScreen);
        _gameMode.StartMode();
        _isInMenu = false;
    }

    public void Restart() => _gameMode.Restart();

    public void ExitToMenu()
    {
        if (!_isInMenu)
        {
            _gameMode.Exit();
            _isInMenu = true;
        }
        else
            ExitFromGame();
    }

    public void ShowLeaderboard() => _leaderboardSystem.OpenLeaderboard(_gameMode.GetValuesForLeaderboard());

    public void ExitFromGame() => Application.Quit();

    private void Poped(PopPeace popPeace)
    {
        _soundController.Pop();
        _gameMode.Poped(popPeace);
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private List<GameMode> _gameModes;

    public void Awake()
    {
        if (_game == null)
            throw new NullReferenceException();
    }

    public void StartGame(int index)
    {
        GameMode mode = _gameModes[index];
        _game.StartGame(mode);
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        _game.Restart();
    }

    public void ExitToMenu()
    {
        _game.ExitToMenu();
        gameObject.SetActive(true);
    }

    public void ShowRecords() => _game.ShowLeaderboard();
    public void Exit() => _game.ExitFromGame();
}

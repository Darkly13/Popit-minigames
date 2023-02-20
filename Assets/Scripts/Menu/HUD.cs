using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private Highscore _highscore;
    [SerializeField] private Button _exitToMenuButton;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private BlockScreen _blockScreen;
    [SerializeField] private TextMeshProUGUI _gameOver;

    public void Awake()
    {
        if (_score == null)
            throw new NullReferenceException();
        if (_highscore == null)
            throw new NullReferenceException();
        if (_exitToMenuButton == null)
            throw new NullReferenceException();
        if (_reloadButton == null)
            throw new NullReferenceException();
        if (_leaderboardButton == null)
            throw new NullReferenceException();
        if (_blockScreen == null)
            throw new NullReferenceException();
        if (_gameOver == null)
            throw new NullReferenceException();
    }

    public void ActivateReloadButton() => _reloadButton.gameObject.SetActive(true);
    public void DeactivateReloadButton() => _reloadButton.gameObject.SetActive(false);
    public void ActivateHighscore() => _highscore.gameObject.SetActive(true);
    public void DeactivateHighscore() => _highscore.gameObject.SetActive(false);
    public void SetScore(float value) => _score.ChangeScore(value);
    public void SetHighscore(float value) => _highscore.ChangeHighscore(value);
    public void Block() => _blockScreen.gameObject.SetActive(true);
    public void Unblock() => _blockScreen.gameObject.SetActive(false);
    public void SetBlockText(string text) => _blockScreen.SetText(text);
    public void ActivateGameOverScreen() => _gameOver.gameObject.SetActive(true);
    public void DeactivateGameOverScreen() => _gameOver.gameObject.SetActive(false);
}

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System;

public  class LeaderboardSystem : MonoBehaviour
{
    private bool _isLoginned=false;

    public void Awake()
    {
        PlayGamesPlatform.Activate();
        Login();
    }


    public void Login()
    {
        Social.localUser.Authenticate(success => 
        {
            if (success)
                _isLoginned = true;
        });
    }

    public void OpenLeaderboard(LeaderboardConfig config)
    {
        if (!_isLoginned)
            Login();
        else
        {
            long highScore = System.Convert.ToInt64(config.Value);
            Social.ReportScore(highScore, config.Key, (bool success) => { });
            Social.ShowLeaderboardUI();
        }
        
    }
}

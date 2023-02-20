using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExitInput : MonoBehaviour
{
    [SerializeField] private Game _game;

    public void Awake()
    {
        if (_game == null)
            throw new NullReferenceException();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _game.ExitToMenu();

        if (Input.GetKeyDown(KeyCode.Home))
            Application.Quit();
    }
}

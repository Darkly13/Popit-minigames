using UnityEngine;
using TMPro;
using System;

public class Highscore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public void Awake()
    {
        if (_textMeshPro == null)
            throw new NullReferenceException();
    }

    public void ChangeHighscore(float value) => _textMeshPro.text = value.ToString();
}

using System;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public void Awake()
    {
        if (_textMeshPro == null)
            throw new NullReferenceException();
    }

    public void ChangeScore(float value) => _textMeshPro.text = value.ToString();
}

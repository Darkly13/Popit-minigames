using System;
using UnityEngine;
using UnityEngine.UI;

public class PopPeaceView : MonoBehaviour
{
    [SerializeField] private PopPeace _model;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _popIn;
    [SerializeField] private Sprite _popOut;

    public void Awake()
    {
        if (_model == null)
            throw new NullReferenceException();
        if (_image == null)
            throw new NullReferenceException();
        if (_popIn == null)
            throw new NullReferenceException();
        if (_popOut == null)
            throw new NullReferenceException();
    }

    private void Poped(PopPeace obj) => _image.sprite = _model.IsPoped ? _popIn : _popOut;

    private void OnEnable() => _model.OnPoped += Poped;

    private void OnDisable() => _model.OnPoped -= Poped; 
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Popit : MonoBehaviour
{
    [SerializeField] private PopPeace _prefab;
    [SerializeField] private int _size;
    [SerializeField] private float _offset;
    [SerializeField] private float _borderOffset;
    [SerializeField] private List<Color> _colors;

    private RectTransform _rectTransform;
    private List<PopPeace> _pops;

    private Vector2 _fieldScale;
    private Vector2 _peaceScale;

    public void OnValidate()
    {
        if (_offset <= 0)
            _offset = 0;
    }

    public void Awake()
    {
        if (_prefab == null)
            throw new NullReferenceException();       
    }

    public List<PopPeace> CreateField()
    {
        _pops = new List<PopPeace>();
        _rectTransform = GetComponent<RectTransform>();
        GenerateGameField();
        return _pops;
    }

    private void GenerateGameField()
    {
        _fieldScale = GetFiledScale();
        _peaceScale = GetPeaceScale(_fieldScale);

        for (int i = 0; i < _size; i++)
        {
            GenerateLine(i);
        }
    }

    private Vector2 GetFiledScale()
    {
        float _fieldWidth = _rectTransform.rect.width;
        float _fieldHeigh = _rectTransform.rect.height;
        return new Vector2(_fieldWidth, _fieldHeigh);
    }

    private Vector2 GetPeaceScale(Vector2 _fieldScale)
    {
        float _peaceWidth = (_fieldScale.x - (_offset * (_size - 1)+2*_borderOffset)) / _size;
        float _peaceHeigh = (_fieldScale.y - (_offset * (_size - 1)+2*_borderOffset)) / _size;
        return new Vector2(_peaceWidth, _peaceHeigh);
    }

    private void GenerateLine(int lineNumber)
    {
        for (int i = 0; i < _size; i++)
        {
            GeneratePeace(lineNumber, i);
        }
    }

    private void GeneratePeace(int lineNumber, int columnNumber)
    {
        PopPeace popPeace = Instantiate(_prefab, transform);
        SetPeaceScale(popPeace);
        Vector2 position = CountRectPosition(lineNumber, columnNumber);
        SetCountedPosition(popPeace, position);
        SetPeaceColor(popPeace, lineNumber);
        _pops.Add(popPeace);
    }

    private void SetPeaceScale(PopPeace popPeace)
    {
        RectTransform rectTransform = popPeace.GetComponent<RectTransform>();
        rectTransform.sizeDelta = _peaceScale;
    }

    private Vector2 CountRectPosition(int lineNumber, int columnNumber)
    {
        float x = -_fieldScale.x / 2 + (_borderOffset + (_offset + _peaceScale.x) * columnNumber + _peaceScale.x / 2);
        float y = _fieldScale.y/2 - (_borderOffset + (_offset + _peaceScale.y) * lineNumber + _peaceScale.x / 2);
        return new Vector2(x, y);
    }

    private void SetCountedPosition(PopPeace popPeace, Vector2 postion)
    {
        RectTransform rectTransform = popPeace.GetComponent<RectTransform>();
        rectTransform.localPosition = postion;

    }

    private void SetPeaceColor(PopPeace popPeace, int lineNumber)
    {
        popPeace.GetComponent<Image>().color = _colors[lineNumber];
    }
}

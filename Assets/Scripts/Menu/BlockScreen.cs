using UnityEngine;
using TMPro;
using System;

public class BlockScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _clip;

    public void Awake()
    {
        if (_text == null)
            throw new NullReferenceException();
        if (_animator == null)
            throw new NullReferenceException();
        if (_clip == null)
            throw new NullReferenceException();
    }

    public void SetText(string text)
    {
        _text.text = text;
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        _animator.Play(_clip.name);
    }
}

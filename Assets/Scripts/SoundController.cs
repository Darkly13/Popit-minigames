using UnityEngine;
using System;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _button;
    [SerializeField] private AudioClip _pop;

    public void Awake()
    {
        if (_audioSource == null)
            throw new NullReferenceException();
        if (_button == null)
            throw new NullReferenceException();
        if (_pop == null)
            throw new NullReferenceException();
    }

    public void Pop()
    {
        _audioSource.PlayOneShot(_pop);
    }

    public void ButtonClick()
    {
        _audioSource.PlayOneShot(_button);
    }
}

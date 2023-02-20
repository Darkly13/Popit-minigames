using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class GuideScreen : MonoBehaviour, IPointerDownHandler
{
    public event Action OnHided;

    [SerializeField] private TextMeshProUGUI _modeName;
    [SerializeField] private TextMeshProUGUI _modeDescription;
    [SerializeField] private Animator _animator;

    public void Awake()
    {
        if (_modeName == null)
            throw new NullReferenceException();
        if (_modeDescription == null)
            throw new NullReferenceException();
    }

    public void Initialize(GuideConfiguration configuration)
    {
        _modeName.text = configuration.Name;
        _modeDescription.text = configuration.Description;
        Show();      
        _animator.Play(configuration.Animation.name);
    }

    public void OnPointerDown(PointerEventData eventData) => Hide();

    private void Show() => gameObject.SetActive(true);

    private void Hide() 
    {
        _animator.Rebind();
        gameObject.SetActive(false);
        OnHided?.Invoke();
    } 
}

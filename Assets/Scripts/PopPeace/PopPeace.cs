using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopPeace : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public event Action<PopPeace> OnPoped;

    public bool IsPoped { get; private set; }

#if UNITY_EDITOR
    public void OnPointerDown(PointerEventData eventData) => Pop();
    public void OnPointerEnter(PointerEventData eventData) { }
#elif UNITY_ANDROID
    public void OnPointerEnter(PointerEventData eventData) => Pop();          
    public void OnPointerDown(PointerEventData eventData) { }
#endif

    public void Pop()
    {
        IsPoped = !IsPoped;
        OnPoped?.Invoke(this);
    }  
}

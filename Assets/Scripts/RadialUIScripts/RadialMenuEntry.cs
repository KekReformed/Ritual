using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TextMeshProUGUI Label;

    [SerializeField]
    RawImage Icon;

    RectTransform Rect;

    private void Start()
    {
        Rect = GetComponent<RectTransform>();
    }
    public void SetLabel(string pText)
    {
        Label.text = pText;
    }

    public void SetIcon(Texture pIcon)
    {
        Icon.texture = pIcon;
    }

    public Texture GetIcon()
    {
        return (Icon.texture);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {  
        Rect.DOComplete();
        Rect.DOScale(Vector3.one * 1.5f, .3f).SetEase(Ease.OutQuad);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Rect.DOComplete();
        Rect.DOScale(Vector3.one,.3f).SetEase(Ease.OutQuad);
    }
}


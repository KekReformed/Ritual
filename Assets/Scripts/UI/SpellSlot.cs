using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    Image _image;

    void Awake()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
    }
    
    public void SetSpellSlot(Sprite icon)
    {
        if (!icon)
        {
            Debug.LogError("No Icon for spell!");
            return;
        }
        
        _image.sprite = icon;
        _image.SetNativeSize();
    }
}

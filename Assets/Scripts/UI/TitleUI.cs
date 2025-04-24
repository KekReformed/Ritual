using System;
using TMPro;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    TMP_Text _text;
    float _timer;
    float _maxTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer >= _maxTime) _text.SetText("");
    }

    public void SetTitle(string text, float displayTime, Color color)
    {
        _text.SetText(text);
        _text.color = color;
        _maxTime = displayTime;
        _timer = 0;
    }
}

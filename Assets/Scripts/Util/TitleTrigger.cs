using System;
using UnityEngine;

public class TitleTrigger : MonoBehaviour
{
    bool triggered;
    public string title;
    public float time;

    void OnTriggerEnter(Collider other)
    {
        if(triggered) return;

        UIManager.Instance.title.SetTitle(title,time,Color.cyan);
        triggered = true;
    }
}

using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string item;

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;

        PlayerManager.Instance.Inventory.Add(item);

        UIManager.Instance.title.SetTitle($"{item} Acquired!",2f,Color.green);
        
        Destroy(this);
    }
}

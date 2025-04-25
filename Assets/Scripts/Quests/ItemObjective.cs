using System;
using UnityEngine;

[Serializable]
public class ItemObjective : IQuestObjective
{

    public string item;
    public bool CheckIfComplete()
    {
        Debug.Log(PlayerManager.Instance.Inventory.Contains(item));
        return PlayerManager.Instance.Inventory.Contains(item);
    }
}

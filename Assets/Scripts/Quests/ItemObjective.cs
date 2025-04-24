using System;
using UnityEngine;

[Serializable]
public class ItemObjective : IQuestObjective
{

    public string item;
    public bool CheckIfComplete()
    {
        return PlayerManager.Instance.Inventory.Contains(item);
    }
}

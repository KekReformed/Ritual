using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();
    public static QuestManager Instance;
    
    void Start()
    {
        Instance = this;
    }

    public static void CheckObjectives()
    {
        if (Quests.Count == 0) return;
        
        bool complete = true;
        foreach (Quest quest in Quests.Values)
        {
            for (int i = 0; i < quest.killObjectives.Length; i++)
            {
                KillObjective killObjective = quest.killObjectives[i];
                if(!killObjective.CheckIfComplete()) complete = false;
            }
            
            for (int i = 0; i < quest.itemObjectives.Length; i++)
            {
                ItemObjective itemObjective = quest.itemObjectives[i];
                
                if(!itemObjective.CheckIfComplete()) complete = false;
            }
            if (complete)
            {
                if (!quest.questGiver.questComplete)
                {
                    quest.QuestEnd();
                }
                quest.questGiver.questComplete = true;
            }
        }
        
    }
}

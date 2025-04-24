using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static Quest Quest;
    public static QuestManager Instance;
    
    void Start()
    {
        Instance = this;
    }

    public static void CheckKillObjectives()
    {
        if (Quest == null) return;
        
        bool complete = true;
        for (int i = 0; i < Quest.killObjectives.Length; i++)
        {
            KillObjective killObjective = Quest.killObjectives[i];
            if(!killObjective.CheckIfComplete()) complete = false;
        }
        if (complete)
        {
            Quest.questGiver.questComplete = true;
        }
    }
}

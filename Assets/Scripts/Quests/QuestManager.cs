using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static Quest Quest;
    [SerializeField] Quest startQuest;

    void Start()
    {
        Quest = startQuest;
    }

    public static void CheckKillObjectives()
    {
        bool complete = true;
        for (int i = 0; i < Quest.KillObjectives.Length; i++)
        {
            KillObjective killObjective = Quest.KillObjectives[i];
            if(!killObjective.CheckIfComplete()) complete = false;
        }
        if(complete) Debug.Log("Quest Complete!");
    }
}

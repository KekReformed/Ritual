using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Quest
{
    public string name;
    public UnityEvent onQuestStart;
    public UnityEvent onQuestEnd;
    public KillObjective[] killObjectives;
    public NPC questGiver;
    public int skillPointReward;

    public void QuestStart()
    {
        onQuestStart.Invoke();
        for (int i = 0; i < killObjectives.Length; i++)
        {
            KillObjective killObjective = killObjectives[i];
            killObjective.QuestBegin();
        }
        Debug.Log("Quest start");
    }

    public void QuestEnd()
    {
        onQuestEnd.Invoke();
        UIManager.Instance.title.SetTitle("Quest Complete!",2f, Color.green);
    }
}

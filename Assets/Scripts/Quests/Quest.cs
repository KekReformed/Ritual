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
    public ItemObjective[] itemObjectives;
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
        for (int i = 0; i < itemObjectives.Length; i++)
        {
            ItemObjective itemObjective = itemObjectives[i];
        }
    }

    public void QuestEnd()
    {
        onQuestEnd.Invoke();
        UIManager.Instance.title.SetTitle("Quest Complete!",2f, Color.green);
        if (!PlayerManager.Instance.completedAQuest)
        {
            UIManager.Instance.title.SetTitle("Quests need to be turned in to their quest giver when completed to earn rewards",5f, Color.cyan);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
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
    }

    public void QuestEnd()
    {
        onQuestEnd.Invoke();
    }
}

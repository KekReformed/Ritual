using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public UnityEvent onQuestStart;
    public UnityEvent onQuestEnd;
    public KillObjective[] KillObjectives;

    public void QuestStart()
    {
        onQuestStart.Invoke();
        for (int i = 0; i < KillObjectives.Length; i++)
        {
            KillObjective killObjective = KillObjectives[i];
            killObjective.QuestBegin();
        }
    }

    public void QuestEnd()
    {
        onQuestEnd.Invoke();
    }
}

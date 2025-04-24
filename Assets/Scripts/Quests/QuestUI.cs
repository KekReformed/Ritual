using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    TMP_Text _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.SetText("");
        string questText = "";
        foreach (Quest quest in QuestManager.Quests.Values)
        {
            if (quest != null)
            {
                questText += $"{quest.name}: \n";
                for (int i = 0; i < quest.killObjectives.Length; i++)
                {
                    KillObjective killObjective = quest.killObjectives[i];
                    questText +=
                        $"Kill {killObjective.count} {killObjective.enemyID}s {Mathf.Min(PlayerManager.PlayerStats.GetStat(killObjective.enemyID, "Kills") - killObjective.enemiesKilledOnStart, killObjective.count).ToString()}/{killObjective.count} \n";
                }
                if (quest.questGiver.questComplete) questText += "Quest Complete! Return to Quest giver";
                    
                _text.SetText(questText);
            }
            else _text.SetText("");
        }
    }
}

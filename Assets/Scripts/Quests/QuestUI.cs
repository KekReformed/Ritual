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
        if (QuestManager.Quest != null)
        {
            for (int i = 0; i < QuestManager.Quest.killObjectives.Length; i++)
            {
                KillObjective killObjective = QuestManager.Quest.killObjectives[i];

                //_text.SetText($"Kill 5 wolves {PlayerManager.PlayerStats.GetStat("Wolf", "Kills").ToString()}/5");
                _text.SetText(
                    $"Kill {killObjective.count} {killObjective.enemyID}s {Mathf.Min(PlayerManager.PlayerStats.GetStat(killObjective.enemyID, "Kills") - killObjective.enemiesKilledOnStart, killObjective.count).ToString()}/{killObjective.count} \n");
            }
        }
        else _text.SetText("");
    }
}

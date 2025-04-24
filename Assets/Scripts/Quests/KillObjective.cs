using System;

[Serializable]
public class KillObjective : IQuestObjective
{
    public int count;
    public string enemyID;
    public int enemiesKilledOnStart;

    public void QuestBegin()
    {
        PlayerManager.PlayerStats.InitialiseStatIfUninitialised(enemyID,"Kills");
        enemiesKilledOnStart = PlayerManager.PlayerStats.GetStat(enemyID, "Kills");
    }
    
    public bool CheckIfComplete()
    {
        return PlayerManager.PlayerStats.GetStat(enemyID, "Kills") >= enemiesKilledOnStart + count;
    }
}

using System;

[Serializable]
public class KillObjective : IQuestObjective
{
    public int count;
    public string enemyID;
    private int _enemiesKilledOnStart;

    public void QuestBegin()
    {
        PlayerManager.PlayerStats.InitialiseStatIfUninitialised(enemyID,"Kills");
        _enemiesKilledOnStart = PlayerManager.PlayerStats.GetStat(enemyID, "Kills");
    }
    
    public bool CheckIfComplete()
    {
        return PlayerManager.PlayerStats.GetStat(enemyID, "Kills") >= _enemiesKilledOnStart + count;
    }
}

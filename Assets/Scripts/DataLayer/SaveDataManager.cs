using System;
using UnityEngine;

public static class SaveDataManager
{
    public static void SaveLevel(string levelID, int checkpoint, int coins, bool unlocked)
    {
        LevelData level = new LevelData();
        level.LevelID = levelID;
        level.CheckPoint = checkpoint;
        level.Coins = coins;
        level.Unlocked = unlocked;

        string levelJson = JsonUtility.ToJson(level);
        PlayerPrefs.SetString(levelID, levelJson);
    }
}

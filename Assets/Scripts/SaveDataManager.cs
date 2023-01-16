using UnityEngine;

public static class SaveDataManager
{
    public static int Coins
    {
        get => PlayerPrefs.GetInt("coins");

        set => PlayerPrefs.SetInt("coins", value);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coinsTotal = 1;
    public int coins = 0;
    public int checkpoints = 0;

    public GameObject completeLevelUI;
    public TMP_Text levelCount;

    [System.NonSerialized]
    public bool finishActive = false;

    private static string currentLevel = "default";
    private static ItemCollector collector;
    private LevelData level;
    private Finish finishObject;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        collector = FindObjectOfType<ItemCollector>();
        finishObject = FindObjectOfType<Finish>();

        if(currentLevel != "MenuPages" && currentLevel != "default")
        {
            // Debug.Log("This is not menu");
            levelCount.text = currentLevel;
            string levelJson = PlayerPrefs.GetString(currentLevel);
            Debug.Log(levelJson);
            level = JsonUtility.FromJson<LevelData>(levelJson);
        }
    }

    public void LoadLevel(string levelName)
    {
        // Check if NOT in menu
        if (currentLevel != "MenuPages" && currentLevel != "default")
        {
            // Currently collected coins
            coins = collector.CoinCount;

            // If level was saved before
            if(level != null)
            {
                // Keep highest value of coins collected ever on this level
                if (level.Coins > coins)
                    coins = level.Coins;
            }
                
            SaveDataManager.SaveLevel(currentLevel, 0, coins, true);
            
            if(levelName != "MenuPages")
                // Use completeLevelUI's code to load next level
                completeLevelUI.SetActive(true);
            else
                // Use plain LoadScene to load MenuPages
                SceneManager.LoadScene(levelName);

            currentLevel = levelName;

            return;
        }

        // If the button is interactable, use this to load levels
        SaveDataManager.SaveLevel(currentLevel, 0, coins, true);
        SceneManager.LoadScene(levelName);
        currentLevel = levelName;
    }

    public void checkFinish(int coinCount)
    {
        if (coinCount >= coinsTotal)
            finishActive = true;
        else
            finishActive = false;

        finishObject.ToggleFinish(finishActive);
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
    }

    public void WinGame()
    {
        completeLevelUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

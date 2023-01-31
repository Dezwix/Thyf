using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Level { get; set; }
    public int Coins { get; set; }

    public GameObject completeLevelUI;

    private static GameManager gameManager;
    private static ItemCollector collector;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        collector = FindObjectOfType<ItemCollector>();

        Coins = PlayerPrefs.GetInt("coins");

        // TODO: Figure out this madness
        //gameManager = gameObject.AddComponent<GameManager>();
    }

    public void LoadLevel(string levelName)
    {
        Coins = collector.CoinCount;
        PlayerPrefs.SetInt("coins", Coins);
        SceneManager.LoadScene(levelName);
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
    }

    public void WinGame()
    {
        completeLevelUI.SetActive(true);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

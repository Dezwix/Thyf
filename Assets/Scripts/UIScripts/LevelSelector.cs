using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public string level;
    public TextMeshProUGUI levelText;
    public bool unlocked = false;

    private Button levelButton;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelText.text = level.ToString();
        levelButton = GetComponent<Button>();

        string levelJson = PlayerPrefs.GetString("Level " + level);
        LevelData levelData = JsonUtility.FromJson<LevelData>(levelJson);

        if ((levelData == null || levelData.Unlocked == false) && level != "0")
        {
            unlocked = false;
            levelButton.interactable = false;
            return;
        }

        levelButton.interactable = true;
        unlocked = true;
    }


        public void OpenScene()
    {
        Time.timeScale = 1f;
        gameManager.LoadLevel("Level " + level);
        //SceneManager.LoadScene("Level " + level);
    }
}

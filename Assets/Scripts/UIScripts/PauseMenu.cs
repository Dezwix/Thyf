using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    public string MenuSceneName = "Menu";

    private PlayerMovement playerMovement;
    private ThrowManager throwManager;
    private GameManager gameManager;

    private void Awake()
    {
        IsPaused = false;
        playerMovement = FindObjectOfType<PlayerMovement>();
        throwManager = FindObjectOfType<ThrowManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("CONTINUE");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        playerMovement.enabled = true;
        throwManager.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        playerMovement.enabled = false;
        throwManager.enabled = false;
    }

    public void LoadMenu()
    {
        gameManager.LoadLevel(MenuSceneName);
        //SceneManager.LoadScene(MenuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

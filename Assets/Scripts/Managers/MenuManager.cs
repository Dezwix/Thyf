using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Refresh Level List!~");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        
        // if first play, go to level 0
        StartGame();

        // else confirm if new game
        // TODO
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Game");
        
        // Start where left off
        StartGame();
    }

    public void Levels()
    {
        Debug.Log(Resources.Load("LevelsAvailable"));
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

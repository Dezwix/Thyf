using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int winScore = 3;
    public GameObject completeLevelUI;

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

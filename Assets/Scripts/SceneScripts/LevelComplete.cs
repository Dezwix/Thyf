using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private Finish finish;

    private void Awake()
    {
        finish = FindObjectOfType<Finish>();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(finish.nextLevel);
    }
}

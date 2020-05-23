using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool autoLoadNextLevel;
    public float autoLoadNextLevelAfter;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // keep screen on
        if (autoLoadNextLevel)
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);

        }
    }
    public void LoadLevel(string name)
    {
        Debug.Log("Level Load requested for " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Debug.Log("QUit game Requested");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}


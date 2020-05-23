using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";
    const string SKIN_KEY = "set_skin";
    const string PLAYER_SCORE = "player_score";
    const string TIMER = "timer";


    public static void SetTime(int time)
    {
        PlayerPrefs.SetInt(TIMER, time);
    }

    public static int GetTimer()
    {
        return PlayerPrefs.GetInt(TIMER);
    }
    // ex.- level_unlocked_2

    public static void SetMasterVolume (float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    
    public static void UnlockLevel (int level)
    {
        ;
        if ( level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // use 1 for true
        }
        else
        {
            Debug.LogError("Trying to unlock level not in build order");
        }
    }
    public static bool IsLevelUnlocked (int level)
    { 
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        bool isLevelUnlocked = (levelValue == 1);

        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // use 1 for true
            return isLevelUnlocked;
        }
        else
        {
            Debug.LogError("Trying to query level not in build order");
            return false;

        }
    }

    public static void SetDifficulty(int difficulty)
    {
        if (difficulty >= 1f && difficulty <= 3f)
        {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficult out of range");
        }
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }

    public static void SetSkin(int skin)
    {
        PlayerPrefs.SetInt(SKIN_KEY, skin);
    }

    public static int GetSkin()
    {
        return PlayerPrefs.GetInt(SKIN_KEY);
    }

    public static void SetScore(int score)
    {
        PlayerPrefs.SetInt(PLAYER_SCORE, score);
    }

    public static int GetScore()
    {
        return PlayerPrefs.GetInt(PLAYER_SCORE);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't Destroy on Load" + name);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        AudioClip thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
        if (!thisLevelMusic)
        {
            return;
        }
        Debug.Log("Music is " + thisLevelMusic);
        Debug.Log(audioSource);
        // if there is some music attached
        audioSource.clip = thisLevelMusic;
        
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeVolume(float volume)
    {
        Debug.Log("Volumne is " + volume);
        audioSource.volume = volume;
    }
}

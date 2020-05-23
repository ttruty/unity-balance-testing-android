using UnityEngine;

// The Audio Source component has an AudioClip option.  The audio
// played in this example comes from AudioClip and is called audioData.

[RequireComponent(typeof(AudioSource))]
public class AudioPlay : MonoBehaviour
{
    AudioSource audioData;
    public AudioClip clip;

    void OnEnable()
    {
        audioData = GetComponent<AudioSource>();
        audioData.PlayOneShot(clip);
        Debug.Log("started");
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Pause"))
        {
            audioData.Pause();
            Debug.Log("Pause: " + audioData.time);
        }

        if (GUI.Button(new Rect(10, 170, 150, 30), "Continue"))
        {
            audioData.UnPause();
        }
    }
}

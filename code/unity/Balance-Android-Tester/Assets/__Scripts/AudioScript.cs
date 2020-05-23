using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip crashSoft;
    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;


    void Awake()
    {

        source = GetComponent<AudioSource>();
    }


    public void Play()
    {
        source.PlayOneShot(crashSoft);
    }

}

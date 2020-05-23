using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSplash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefsManager.SetMasterVolume(.5f);
        PlayerPrefsManager.SetDifficulty(1);
        PlayerPrefsManager.SetTime(60);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

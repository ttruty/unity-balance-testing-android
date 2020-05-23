using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{

    public TMP_InputField timerField;
    public LevelManager levelManager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetTime(Int32.Parse(timerField.text));
        levelManager.LoadLevel("Menu");
    }

    public void SetDefaults()
    {
    }
}

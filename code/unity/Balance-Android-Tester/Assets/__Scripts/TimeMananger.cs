using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeMananger : MonoBehaviour
{
    float currCountdownValue;
    public TMP_Text timerText;
    public ScorePoints score;
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        //timerText = GetComponent<TMP_Text>();
        int time = PlayerPrefsManager.GetTimer();
        Debug.Log(time);
        StartCoroutine(StartCountdown(time));
    }

    // Update is called once per frame
    void Update()
    {            


    }

    public IEnumerator StartCountdown(float countdownValue = 60)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            timerText.SetText("Timer: " + currCountdownValue.ToString());
            yield return new WaitForSeconds(1.0f);            
            currCountdownValue--;
            
        }
        Debug.Log("Game Ended");
        Debug.Log("Score: " + score.GetPoints());
        PlayerPrefsManager.SetScore(score.GetPoints());
        levelManager.LoadLevel("Menu");
        
        

    }
}

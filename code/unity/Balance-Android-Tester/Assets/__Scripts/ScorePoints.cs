using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePoints : MonoBehaviour
{

    public TMP_Text scoreText;

    private int Points;

    public void Start()
    {
        Points = 0;
        scoreText.text = "Score: " + Points;
    }

    public void AddPoints(int amount)
    {
        Points = Points + amount;
        scoreText.SetText("Score: " + Points.ToString());
    }

    public int GetPoints()
    {
        return Points;
    }

}

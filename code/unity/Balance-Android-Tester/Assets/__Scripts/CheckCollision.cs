using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D boxCollider;
    public float timerCountDown = 5.0f;
    public ScorePoints points;

    private TMP_Text mText;
    void Start()
    {
        mText = GetComponentInChildren<TMP_Text>().GetComponent<TMP_Text>();
        points = GameObject.Find("ScoreManager").GetComponent<ScorePoints>();

    }

    // Update is called once per frame
    void Update()
    {
       
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
        if (overlap.Length > 1)
        {
            double seconds = Math.Round(timerCountDown, 1);
            mText.SetText(seconds.ToString());
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
            timerCountDown -= Time.deltaTime;

            Debug.Log(string.Format("Found {0} overlapping object(s)", overlap.Length - 1));
            Debug.Log("Timer: " + timerCountDown);
            if (timerCountDown <= 0)
            {
                Destroy(gameObject);
                points.AddPoints(1); //add points
            }
           
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        


    }

}

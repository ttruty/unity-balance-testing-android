using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSensor : MonoBehaviour
{
    public BluetoothDevice leftFoot;
    public BluetoothDevice rightFoot;

    public float rToe;
    public float lToe;
    public float rHeel;
    public float lHeel;

    public float sideToSide;
    public float frontToBack;


    // Start is called before the first frame update
    void Start()
    {
        rToe = rightFoot.ToeData;
        lToe = leftFoot.ToeData;
        rHeel = rightFoot.HeelData;
        lHeel = leftFoot.HeelData;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!Application.isEditor) //take bt input if not in editor
        {
            rToe = MapToScreen(rightFoot.ToeData);
            lToe = MapToScreen(leftFoot.ToeData);

            rHeel = MapToScreen(rightFoot.HeelData);
            lHeel = MapToScreen(leftFoot.HeelData);
        }



        this.sideToSide = (rHeel + rToe) - (lHeel + lToe);
        this.frontToBack = (rToe + lToe) - (lHeel + rHeel);
    }

    float MapToScreen(float value)
    {

        return (value / 100);
    }
}

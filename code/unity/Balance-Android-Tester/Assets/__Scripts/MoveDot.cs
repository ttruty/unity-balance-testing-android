using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDot : MonoBehaviour
{
    public int xTransform;
    public int yTransform;
    private Vector3 currentPos;
    public FootSensor footSensor;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
        //update the position
        Vector3 newPos = new Vector3(currentPos.x + xTransform, currentPos.y + yTransform, currentPos.z);

        //Debug.Log("Left Toe:" + footSensor.lToe);
        //Debug.Log("Right Toe:" + footSensor.rToe);
        //Debug.Log("Right Heel:" + footSensor.lHeel);
        //Debug.Log("Left Heel:" + footSensor.rHeel);
        //Debug.Log("Front Balance:" + footSensor.frontToBack);
        //Debug.Log("Side Balance:" + footSensor.sideToSide);

        transform.position = newPos;
    }
}

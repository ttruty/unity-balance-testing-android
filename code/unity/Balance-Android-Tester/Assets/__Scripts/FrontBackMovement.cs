using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBackMovement : MonoBehaviour
{
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
        Vector3 newPos = new Vector3(currentPos.x, currentPos.y + footSensor.sideToSide, currentPos.z);

        transform.position = newPos;

        Debug.Log("Side2Side: " + footSensor.sideToSide);
    }
}

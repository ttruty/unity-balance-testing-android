using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSideMovement : MonoBehaviour
{
    private Vector3 currentPos;
    public FootSensor footSensor;
    Camera m_MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane - 10));
        currentPos = transform.position;
        m_MainCamera = Camera.main;
        //camera = GetComponent<Camera>();


    }

    // Update is called once per frame
    void Update()
    {
        //update the position
        var bottomLeft = m_MainCamera.ScreenToWorldPoint(Vector3.zero);
        var topRight = m_MainCamera.ScreenToWorldPoint(new Vector3(
            m_MainCamera.pixelWidth, m_MainCamera.pixelHeight));

        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);

        Vector3 newPos = new Vector3(currentPos.x + footSensor.sideToSide, currentPos.y + footSensor.frontToBack, currentPos.z);
        
        transform.position = newPos;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
                Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
                transform.position.z);
        //Debug.Log("X:" + transform.position.x + "Y:" + transform.position.y);
        // Debug.Log("Side2Side: " + footSensor.sideToSide);
    }
}

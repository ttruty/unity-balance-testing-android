using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClearBluetoothConnections : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("BluetoothConnect"));
        BluetoothLEHardwareInterface.DisconnectAll();
        BluetoothLEHardwareInterface.DeInitialize(() => { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

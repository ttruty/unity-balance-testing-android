using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothDeinitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestoyBT()
    {
        Destroy(GameObject.Find("BluetoothConnect"));
        BluetoothLEHardwareInterface.DisconnectAll();
        BluetoothLEHardwareInterface.DeInitialize(() => { });
        
    }
}

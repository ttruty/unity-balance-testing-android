using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothStatus : MonoBehaviour
{
    public GameObject btGameObject;
    public Canvas canvas;
    public TMPro.TMP_Text[] text;
    public TMPro.TMP_Text[] reading;

    private BluetoothConnect m_btConnect;
    private List<BluetoothDevice> m_connectedDevices;
    private string _dataString;

    // Start is called before the first frame update
    void Start()
    {
        m_btConnect = btGameObject.GetComponent<BluetoothConnect>();
        m_connectedDevices = m_btConnect.m_devices;
        
        for (int i = 0; i < m_connectedDevices.Count; i++)
        {
                text[i].SetText(m_connectedDevices[i].Name);
            BluetoothLEHardwareInterface.Log("STRING NAME: " + m_connectedDevices[i].Name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_btConnect.isConnected)
        {
            //text.SetText("Connected to: " + m_btConnect);
            //Object.Destroy(canvas, 5.0f);

            for (int i = 0; i < m_connectedDevices.Count; i++)
            {
                reading[i].SetText(m_connectedDevices[i].Data);
                BluetoothLEHardwareInterface.Log("STRING DATA: " + m_connectedDevices[i].Data);
            }

        }
    }
}

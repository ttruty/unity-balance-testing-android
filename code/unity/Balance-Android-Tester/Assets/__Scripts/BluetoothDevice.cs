using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothDevice : MonoBehaviour
{
    public string AddressToConnect;

    private string m_name;
    private string m_address;
    private int m_toeData;
    private int m_heelData;
    private string m_data;

    public string Name {
        get
        {
            return m_name;
        }
        set
        {
            m_name = value;
        }
    }
    public string Address
    {
        get
        {
            return m_address;
        }
        set
        {
            m_address = value;
        }
    }
    public int ToeData
    {
        get
        {
            return m_toeData;
        }
        set
        {
            m_toeData = value;
        }
    }
    public int HeelData
    {
        get
        {
            return m_heelData;
        }
        set
        {
            m_heelData = value;
        }
    }
    public string Data
    {
        get { return m_data; }
        set { m_data = value; }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;


public class BluetoothConnect : MonoBehaviour
{
    // ----------------------------------------------------------------- 
    // change these to match the bluetooth device you're connecting to: 
    // ----------------------------------------------------------------- 

    //6E400001-B5A3-F393-E0A9-E50E24DCCA9E
    private string _FullUID = "6e40****-b5a3-f393-e0a9-e50e24dcca9e"; //Default UUID from adaFruit nrf52
    private string _serviceUUID = "0001"; // service characteristic
    private string _readCharacteristicUUID = "0003";
    private string _writeCharacteristicUUID = "0002";

    // bools to hold status of connection and charateristics
    public bool isConnected = false;
    private bool _readFound = false;
    private bool _writeFound = false;
    private List<string> _connectedID = null;

    //Lists for all found and connected devices
    private Dictionary<string, string> _peripheralList; // found devices
    private Dictionary<string, bool> _connectionList; // connected devices
    public List<BluetoothDevice> m_devices; // bluetooth objects
    private float _subscribingTimeout = 0f; //bt timeout setting

    public string[] devicesToConnect;
    private String strForce; // holder for force input

    //Peripheal objects to hold the bt info and device readings
    public GameObject leftFoot;
    private BluetoothDevice oculusLeft;
    public GameObject rightFoot;
    private BluetoothDevice oculusRight;

    // Use this for initialization 
    void Start()
    {
        oculusLeft = leftFoot.GetComponent<BluetoothDevice>();
        oculusRight = rightFoot.GetComponent<BluetoothDevice>();
        
        Debug.Log("Starting");
        BluetoothLEHardwareInterface.Initialize(true, false, () => { },
                                      (error) => { }
        );
        Invoke("scan", 1f);
        

    }

    // Update is called once per frame 
    void Update()
    {
        if (_readFound && _writeFound)
        {
            _readFound = false;
            _writeFound = false;
            _subscribingTimeout = 1.0f;
        }

        if (_subscribingTimeout > 0f)
        {
            _subscribingTimeout -= Time.deltaTime;
            if (_subscribingTimeout <= 0f)
            {
                _subscribingTimeout = 0f;
                foreach (string id in _connectedID)
                { 
                BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
                   id, FullUUID(_serviceUUID), FullUUID(_readCharacteristicUUID),
                   (deviceAddress, notification) => {
                   },
                   (deviceAddress2, characteristic, data) => {
                       BluetoothLEHardwareInterface.Log("id: " + id);
                       if (deviceAddress2.CompareTo(id) == 0)
                       {
                           BluetoothLEHardwareInterface.Log(string.Format("data length: {0}", data.Length));
                           if (data.Length == 0)
                           {
                               // do nothing 
                           }
                           else
                           {
                               //string stringData = ASCIIEncoding.UTF8.GetString(data); //if mc give us strings
                               string byteData = ByteArrayToString(data); // if string in bytes
                               int intData = HexToInt(byteData);
                               BluetoothLEHardwareInterface.Log("data: " + byteData);
                               strForce = byteData;

                               string heelString = strForce.Substring(4, 4);
                               string toeString = strForce.Substring(0, 4);
                               int toeInt = HexToInt(toeString);
                               int heelInt = HexToInt(heelString);

                               //Debugging In the Canvas
                               if (id == oculusLeft.AddressToConnect)                             

                               {
                                   oculusLeft.HeelData = heelInt;
                                   oculusLeft.ToeData = toeInt;
                                   oculusLeft.Data = strForce;
                                   //nameTMP[0].SetText(id);
                                   //dataTMP[0].SetText(strForce);
                                   //toeTMP[0].SetText(toeString);
                                   //toeTMP[2].SetText(toeInt.ToString());
                                   //heelTMP[0].SetText(heelString);
                                   //heelTMP[2].SetText(heelInt.ToString());
                                   //DEBUG
                                   //DebugBluetooth debugText = new DebugBluetooth();
                               }
                               if (id == oculusRight.AddressToConnect)
                               {
                                   oculusRight.HeelData = heelInt;
                                   oculusRight.ToeData = toeInt;
                                   oculusRight.Data = strForce;
                                   //nameTMP[1].SetText(id);
                                   //dataTMP[1].SetText(strForce);
                                   //toeTMP[1].SetText(toeString);
                                   //toeTMP[3].SetText(toeInt.ToString());
                                   //heelTMP[1].SetText(heelString);
                                   //heelTMP[3].SetText(heelInt.ToString());

                                   //DebugBluetooth debugText = new DebugBluetooth();

                               }

                           }
                       }
                   });

                 }

            }
        }
    }


    public string ReadBytes()
    {
        if (_readFound && _writeFound)
        {
            _readFound = false;
            _writeFound = false;
            _subscribingTimeout = 1.0f;
        }

        if (_subscribingTimeout > 0f)
        {
            _subscribingTimeout -= Time.deltaTime;
            if (_subscribingTimeout <= 0f)
            {
                _subscribingTimeout = 0f;
                foreach (string id in _connectedID)
                {
                    BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
                       id, FullUUID(_serviceUUID), FullUUID(_readCharacteristicUUID),
                       (deviceAddress, notification) =>
                       {
                       },
                       (deviceAddress2, characteristic, data) =>
                       {
                           BluetoothLEHardwareInterface.Log("id: " + id);
                           if (deviceAddress2.CompareTo(id) == 0)
                           {
                               BluetoothLEHardwareInterface.Log(string.Format("data length: {0}", data.Length));
                               if (data.Length == 0)
                               {
                               // do nothing 
                           }
                               else
                               {
                              // string stringData = ASCIIEncoding.UTF8.GetString(data); //if mc give us strings
                               string byteData = ByteArrayToString(data); // if string in bytes
                               BluetoothLEHardwareInterface.Log("data: " + byteData);
                                   strForce = byteData;
                                   foreach(BluetoothDevice _device in m_devices)
                                   {
                                       if(_device.Name == id)
                                       {
                                           _device.Data = strForce;
                                       }
                                   }

                               }
                           }
                       });
                }

            }
        }
        return strForce;
    }

    /// <summary>
    /// Convert bytearray to string
    /// </summary>
    /// <param name="ba"></param>
    /// <returns></returns>
    public static string ByteArrayToString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
            
        }
        return hex.ToString();
    }

    public static int HexToInt(string hex)
    {
        int decValue = int.Parse(hex.ToString(), System.Globalization.NumberStyles.HexNumber);
        return decValue;
    }

    public string[] getForce()
    {
        string toeValue = strForce.Substring(0, 4);
        string heelValue = strForce.Substring(4, 4);
        string[] forceReading = { toeValue, heelValue, strForce };
        return forceReading;
    }

    void sendDataBluetooth(string sData)
    {
        if (sData.Length > 0)
        {
            byte[] bytes = ASCIIEncoding.UTF8.GetBytes(sData);
            if (bytes.Length > 0)
            {
                sendBytesBluetooth(bytes);
            }
        }
    }

    void sendBytesBluetooth(byte[] data)
    {
        foreach (string id in _connectedID)
        {
            BluetoothLEHardwareInterface.Log(string.Format("data length: {0} uuid {1}", data.Length.ToString(), FullUUID(_writeCharacteristicUUID)));
            BluetoothLEHardwareInterface.WriteCharacteristic(id, FullUUID(_serviceUUID), FullUUID(_writeCharacteristicUUID),
               data, data.Length, true, (characteristicUUID) =>
               {
                   BluetoothLEHardwareInterface.Log("Write succeeded");
               }
            );
        }
    }


    void scan()
    {

        // the first callback will only get called the first time this device is seen 
        // this is because it gets added to a list in the BluetoothDeviceScript 
        // after that only the second callback will get called and only if there is 
        // advertising data available 
        
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) => {
            AddPeripheral(name, address);
        }, (address, name, rssi, advertisingInfo) => { });



    }
    /// <summary>
    /// Add the peripheral device to the connected objcects
    /// </summary>
    /// <param name="name"></param>
    /// <param name="address"></param>
    void AddPeripheral(string name, string address)
    {
        if (_peripheralList == null)
        {
            _peripheralList = new Dictionary<string, string>();
        }
        if (!_peripheralList.ContainsKey(address))
        {
            _peripheralList[address] = name;
            foreach (string device in devicesToConnect)
            {
                if (name.Trim().ToLower() == device.Trim().ToLower())
                {
                    //txtDebug.text += "Found our device, stop scanning \n"; 
                    //BluetoothLEHardwareInterface.StopScan (); 
                    connectBluetooth(address);
                    if (_connectionList == null)
                    {
                        _connectionList = new Dictionary<string, bool>();
                        _connectionList[name] = true;
                    }
                    if (m_devices == null)
                    {
                        BluetoothDevice _newConnection = new BluetoothDevice();
                        m_devices.Add(_newConnection);
                        if (oculusRight.AddressToConnect == address)
                        {
                            oculusRight.Name = name;
                            oculusRight.Address = address;
                        }
                        if (oculusLeft.AddressToConnect == address)
                        {
                            oculusLeft.Name = name;
                            oculusLeft.Address = address;
                        }

                    }

                }
            }
        }
    }

    void connectBluetooth(string addr)
    {
        BluetoothLEHardwareInterface.ConnectToPeripheral(addr, (address) => {
        },
           (address, serviceUUID) => {
           },
           (address, serviceUUID, characteristicUUID) => {

               // discovered characteristic 
               if (IsEqual(serviceUUID, _serviceUUID))
               {
                   if (_connectedID == null)
                   {
                       _connectedID = new List<string>();
                   }
                   _connectedID.Add(address);
                   isConnected = true;

                   if (IsEqual(characteristicUUID, _readCharacteristicUUID))
                   {
                       _readFound = true;
                   }
                   if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
                   {
                       _writeFound = true;
                   }

                   //DeInitialize();

               }
           }, (address) => {

               // this will get called when the device disconnects 
               // be aware that this will also get called when the disconnect 
               // is called above. both methods get call for the same action 
               // this is for backwards compatibility 
               isConnected = false;
           });

    }

    void StopScan()
    {
        BluetoothLEHardwareInterface.StopScan();
    }


    void sendData()
    {
        return;
    }

    // ------------------------------------------------------- 
    // some helper functions for handling connection strings 
    // ------------------------------------------------------- 
    string FullUUID(string uuid)
    {
        return _FullUID.Replace("****", uuid);
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
        {
            uuid1 = FullUUID(uuid1);
        }
        if (uuid2.Length == 4)
        {
            uuid2 = FullUUID(uuid2);
        }
        return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
    }

}
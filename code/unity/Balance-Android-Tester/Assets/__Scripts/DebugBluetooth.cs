using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugBluetooth : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    // Start is called before the first frame update
    //Debugging output
    public TMPro.TMP_Text nameTMP;
    public TMPro.TMP_Text dataTMP;
    public TMPro.TMP_Text toeTMP;
    public TMPro.TMP_Text heelTMP;


    void Start()
    {
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DebugText(string id, string strForce, int toeInt, int heelInt)
    {
        nameTMP.SetText(id);
        dataTMP.SetText(strForce);
        toeTMP.SetText(toeInt.ToString());
        heelTMP.SetText(heelInt.ToString());
    }
}

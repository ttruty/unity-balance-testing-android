using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DebugController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TMP_Text dpadText;
    public TMPro.TMP_Text gripText;
    //public TMPro.TextMeshPro p2dText;
    //public TMPro.TextMeshPro thumbText;
    public TMPro.TMP_Text triggerText;
    private List<InputDevice> foundControllers;
    private List<InputFeatureUsage> inputFeatures;

    void Start()
    {
        InputDeviceCharacteristics rightTrackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right, rightHandedControllers;

        foundControllers = new List<InputDevice>();
        var inputFeatures = new List<InputFeatureUsage>();
        InputDevices.GetDevicesWithCharacteristics(rightTrackedControllerFilter, foundControllers);

        dpadText.SetText(foundControllers[0].name);
        gripText.SetText(foundControllers[0].characteristics.ToString());

        string inputString = "";
        if (foundControllers[0].TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                inputString += feature.name;
                inputString += ":";
                inputString += feature.type;
                inputString += ":";
                if (feature.type == typeof(bool))
                {
                    bool featureValue;
                    if (foundControllers[0].TryGetFeatureValue(feature.As<bool>(), out featureValue))
                    {
                        inputString += featureValue.ToString();
                    }
                }
                //if (feature.type == typeof(Vector2))
                //{
                //    bool featureValue;
                //     if(foundControllers[0].TryGetFeatureValue(feature.As<bool>(), out featureValue))
                //    {
                //        inputString += featureValue.ToString();
                //    }
                //}


            }
            triggerText.SetText(inputString);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}

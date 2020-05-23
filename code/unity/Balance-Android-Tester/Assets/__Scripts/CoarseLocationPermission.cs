using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class CoarseLocationPermission : MonoBehaviour
{
    GameObject dialog = null;

    void Start()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            dialog = new GameObject();
        }
#endif
    }

    void OnGUI()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            // The user denied permission to use the microphone.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            dialog.AddComponent<PermissionsRationaleDialog>();
            return;
        }
        else if (dialog != null)
        {
            Destroy(dialog);
        }
#endif

        // Now you can do things with the microphone
    }
}

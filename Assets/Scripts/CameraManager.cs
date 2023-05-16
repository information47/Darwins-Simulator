using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            DisableAllCamerasExceptMainCamera();
        }
    }
    private void DisableAllCamerasExceptMainCamera()
    {
        // Récupérer toutes les caméras actives dans la scène
        Camera[] allCameras = FindObjectsOfType<Camera>();

        foreach (Camera camera in allCameras)
        {
            if (camera != Camera.main)
            {
                // Désactiver la caméra si elle n'est pas la caméra principale
                camera.enabled = false;
            }
        }
    }
}

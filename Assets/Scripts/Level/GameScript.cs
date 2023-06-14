using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public static GameScript Instance { get; private set; }

    public bool gamePlaying;
    public bool gamePaused;
    private Camera currentCamera;

    private void Awake() // Pour camera switch
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchCamera(Camera newCamera)
    {
        // Désactive la caméra courante
        if (currentCamera != null)
        {
            currentCamera.enabled = false;
        }

        // Active la nouvelle caméra et la définit comme caméra courante
        newCamera.enabled = true;
        currentCamera = newCamera;
    }

    // Gère l'apparition et disparition de l'écran de stats
    private List<PopupController> activePopups = new List<PopupController>();

    public void RegisterPopup(PopupController popupController)
    {
        activePopups.Add(popupController);
    }

    public void UnregisterPopup(PopupController popupController)
    {
        activePopups.Remove(popupController);
    }

    public void HideAllPopups()
    {
        // Faire une copie de la liste pour l'énumération
        var popupsToHide = new List<PopupController>(activePopups);

        foreach (var popupController in popupsToHide)
        {
            popupController.HidePopup();
        }

        // Vous pouvez vider la liste originale maintenant
        activePopups.Clear();
    }

    private void Start()
    {
        gamePlaying = false;
        gamePaused = false;
    }
}

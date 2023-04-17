using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    [SerializeField] private CanvasGroup MainsMenuCanvas;
    [SerializeField] private CanvasGroup GameInterfaceCanvas;

    public void ShowMenu()
    {
        Debug.Log("ShowMenu");
        // Rends le Canva visible en mettant sa valeur alpha à 1
        MainsMenuCanvas.alpha = 1;
        // Stop la mise à jour des objets du jeu en mettant timeScale à 0
        Time.timeScale = 0f;
    }

    public void HideMenu()
    {
        Debug.Log("HideMenu");
        // Rends le Canva non visible en mettant sa valeur alpha à 0
        MainsMenuCanvas.alpha = 0;
        // Relance la mise à jour des objets du jeu en mettant timeScale à 1
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void Settings()
    {
        Debug.Log("Parametres");
    }

    public void Sound()
    {
        Debug.Log("Sound");
    }

    private void Start()
    {
        // Stop le jeu tant que le joueur n'appuis pas sur PLAY
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            HideMenu();
        }
    }
}

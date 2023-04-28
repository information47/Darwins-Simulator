using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MainMenuSave : MonoBehaviour
{
    [SerializeField] private Canvas MenuCanva;
    [SerializeField] private Canvas InterfaceCanva;

    [SerializeField] private CanvasGroup MainsMenuCanvas;
    [SerializeField] private CanvasGroup GameInterfaceCanvas;
    string gameState;

    public void ShowMenu()
    {
        Debug.Log("ShowMenu");
        // Rends le Canva visible en mettant sa valeur alpha à 1
        ChangeGameState("Menu");
        // Stop la mise à jour des objets du jeu en mettant timeScale à 0
        Time.timeScale = 0f;
    }

    public void HideMenu()
    {
        Debug.Log("HideMenu");
        // Rends le Canva non visible en mettant sa valeur alpha à 0
        ChangeGameState("Game");
        // Relance la mise à jour des objets du jeu en mettant timeScale à 1
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame");
        // Rends le Canva visible en mettant sa valeur alpha à 1
        ChangeGameState("Pause");
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
        ChangeGameState("Menu");
    }

    private void ChangeGameState(string gameStateChange)
    {
        if (gameStateChange == "Menu")
        {
            MainsMenuCanvas.alpha = 1;
            MenuCanva.sortingOrder = 1;
            InterfaceCanva.sortingOrder = 0;
            GameInterfaceCanvas.alpha = 0;
            gameState = "Menu";
        }
        else if (gameStateChange == "Game")
        {
            MainsMenuCanvas.alpha = 0;
            MenuCanva.sortingOrder = 0;
            InterfaceCanva.sortingOrder = 1;
            GameInterfaceCanvas.alpha = 1;
            gameState = "Game";
        }
        else if (gameStateChange == "Pause")
        {
            if(gameState == "Pause")
            {
                Time.timeScale = 1f;
                gameState = "Game";
            }
            else
            {
                Time.timeScale = 0f;
                gameState = "Pause";
            }
        }
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

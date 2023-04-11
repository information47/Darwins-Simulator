using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        Debug.Log("Start Game");
        //SceneManager.LoadScene(sceneName);

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

    public void FocusMainCamera()
    {
        // Trouver la cam�ra principale dans la sc�ne
        Camera mainCamera = Camera.main;

        // V�rifier si la cam�ra a �t� trouv�e
        if (mainCamera != null)
        {
            // Changer la position de la cam�ra actuelle � celle de la cam�ra principale
            if (Camera.current != null)
            {
                Camera.current.transform.position = mainCamera.transform.position;
                Camera.current.transform.rotation = mainCamera.transform.rotation;
            }
            else
            {
                Debug.LogError("La cam�ra actuelle n'a pas �t� trouv�e.");
            }
        }
        else
        {
            Debug.LogError("La cam�ra principale n'a pas �t� trouv�e dans la sc�ne.");
        }
    }

}

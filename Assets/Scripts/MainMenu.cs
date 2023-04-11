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
        // Trouver la caméra principale dans la scène
        Camera mainCamera = Camera.main;

        // Vérifier si la caméra a été trouvée
        if (mainCamera != null)
        {
            // Changer la position de la caméra actuelle à celle de la caméra principale
            if (Camera.current != null)
            {
                Camera.current.transform.position = mainCamera.transform.position;
                Camera.current.transform.rotation = mainCamera.transform.rotation;
            }
            else
            {
                Debug.LogError("La caméra actuelle n'a pas été trouvée.");
            }
        }
        else
        {
            Debug.LogError("La caméra principale n'a pas été trouvée dans la scène.");
        }
    }

}

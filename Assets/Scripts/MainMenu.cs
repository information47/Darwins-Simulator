using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        Debug.Log("Le joueur a quitté le jeu.");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Le joueur a quitté le jeu.");
    }
}

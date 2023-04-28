using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject configMenu;
    public GameObject pauseMenu;
    public GameObject gameInterface;

    public void GoToMenu()
    {
        // ICI METTRE UNE FONCTION QUI RESET LE JEU

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToConfig()
    {
        mainMenu.SetActive(false);
        configMenu.SetActive(true);
    }

}

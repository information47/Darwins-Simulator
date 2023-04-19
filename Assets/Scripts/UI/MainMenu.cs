using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameInterface;

    private void Start()
    {
    }

    public void GoToMenu()
    {
        // ICI METTRE UNE FONCTION QUI RESET LE JEU
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
/*
        Time.timeScale = 0f;
        // Désactive le menu pause avant de revenir au menu principal
        pauseMenu.SetActive(false);
        // Active le menu principal
        mainMenu.SetActive(true);*/
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameInterface.SetActive(true);
        Time.timeScale = 1f;
    }



    private void Update()
    {
    }
}

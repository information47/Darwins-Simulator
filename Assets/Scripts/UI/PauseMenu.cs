using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inGameInterface;
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseGame()
    {
        inGameInterface.SetActive(false);
        pauseMenu.SetActive(true);
        game.GetComponent<GameScript>().gamePaused = true;
        Time.timeScale = 0f;

        // Set gamePlaying to true
        game.GetComponent<GameScript>().gamePlaying = false;
    }
    public void ResumeGame()
    {
        Debug.Log("ResumePause");
        pauseMenu.SetActive(false);
        inGameInterface.SetActive(true);
        game.GetComponent<GameScript>().gamePaused = false;
        Time.timeScale = 1f;

        // Set gamePlaying to false
        game.GetComponent<GameScript>().gamePlaying = true;
    }
}

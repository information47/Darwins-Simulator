using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inGameInterface;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame");
        //inGameInterface.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Debug.Log("ResumePause");
        pauseMenu.SetActive(false);
        //inGameInterface.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f;
    }
}

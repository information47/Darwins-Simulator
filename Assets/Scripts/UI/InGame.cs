using QuantumTek.QuantumUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public QUI_OptionList optionList;
    public bool isInGame;
  
    // Start is called before the first frame update
    void Start()
    {
        optionList.onChangeOption.AddListener(GameSpeed);
        isInGame = true;
    }

    // Tant que la game interface est active, on prends en compte la touche ESPACE pour mettre le jeu en pause
    // car si game interface n'est pas active, l'update ne fonctionnera pas
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPE");
            if (pauseMenu.isPaused == true)
            {
                pauseMenu.ResumeGame();
                pauseMenu.isPaused = false;
            }
            else if(pauseMenu.isPaused == false)
            {
                pauseMenu.PauseGame();
                pauseMenu.isPaused = true;
            }
        }
    }

    public void GameSpeed()
    {
        if (optionList.optionIndex == 0)
        {
            Time.timeScale = 0.25f;
        }
        else if (optionList.optionIndex == 1)
        {
            Time.timeScale = 0.5f;
        }
        else if (optionList.optionIndex == 2)
        {
            Time.timeScale = 1f;
        }
        else if (optionList.optionIndex == 3)
        {
            Time.timeScale = 2f;
        }
        else if (optionList.optionIndex == 4)
        {
            Time.timeScale = 4f;
        }
        else if (optionList.optionIndex == 5)
        {
            Time.timeScale = 8f;
        }
        else if (optionList.optionIndex == 6)
        {
            Time.timeScale = 16f;
        }
        else if (optionList.optionIndex == 7)
        {
            Time.timeScale = 32f;
        }
        else if (optionList.optionIndex == 8)
        {
            Time.timeScale = 64f;
        }
    }
}

using QuantumTek.QuantumUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    PauseMenu pauseMenu = new PauseMenu();
    public QUI_OptionList optionList;
  
    // Start is called before the first frame update
    void Start()
    {
        optionList.onChangeOption.AddListener(GameSpeed);
    }

    // Tant que la game interface est active, on prends en compte la touche ESPACE pour mettre le jeu en pause
    // car si game interface n'est pas active, l'update ne fonctionnera pas
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPE");
            if (pauseMenu.isPaused)
            {
                pauseMenu.ResumeGame();
            }
            else
            {
                pauseMenu.PauseGame();
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

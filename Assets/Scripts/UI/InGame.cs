using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    PauseMenu pauseMenu = new PauseMenu();
    // Start is called before the first frame update
    void Start()
    {
        
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
}

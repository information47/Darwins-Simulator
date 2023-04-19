using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class UI : MonoBehaviour
{
    PauseMenu pauseMenu = new PauseMenu();

    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.isPaused = false;
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public bool gamePlaying;
    public bool gamePaused;

    private void Start()
    {
        gamePlaying = false;
        gamePaused = false;
    }
}

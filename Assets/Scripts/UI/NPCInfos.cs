using QuantumTek.QuantumUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInfos : MonoBehaviour
{
    public GameObject npcInfosUI;
    public InGame inGame;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (inGame.isInGame == false)
        {
            npcInfosUI.SetActive(false);
        }
    }

    public void ShowNPCInfos()
    {
        if (inGame.isInGame == true)
        {
            npcInfosUI.SetActive(true);
        }
    }
    public void CloseNPCInfos()
    {
        npcInfosUI.SetActive(false);
    }
}
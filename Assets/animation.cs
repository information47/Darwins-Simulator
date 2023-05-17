using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    [Header("unity Setup")]
    public float time;
  //  private GameObject[] Npc = GameObject.FindGameObjectsWithTag("NPC");

    void start()
    {
        Destroy(gameObject, time);
    }
}

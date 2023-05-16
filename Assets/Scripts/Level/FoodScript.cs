using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public float Timer = 0;

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 15000)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("NPC"))
        {
            Destroy(this.gameObject);
        }
    }
}

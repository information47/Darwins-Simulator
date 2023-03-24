using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Boolean Jump = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bonjour START");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -5);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-5, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
        }
    }
}

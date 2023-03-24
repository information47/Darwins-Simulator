using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bonjour START");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Déplacements hortitaux et verticaux - horizontalInput retourne -1, 0 ou 1
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        // JUMP
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.y);
        }
    }
}

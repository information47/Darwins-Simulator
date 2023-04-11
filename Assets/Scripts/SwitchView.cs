using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchView : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public GameObject[] targets;
    private int currentTargetIndex;

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("NPC");
        currentTargetIndex = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            Transform target = targets[currentTargetIndex].transform;
            thirdPersonCamera.transform.position = target.Find("Camera Pivot").position;
            thirdPersonCamera.transform.rotation = target.Find("Camera Pivot").rotation;
        }
    }
}
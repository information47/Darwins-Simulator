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
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length; Debug.Log(" 1 currentTargetIndex : " + currentTargetIndex);
            Transform target = targets[currentTargetIndex].transform; Debug.Log("2 Target :" + target );
            thirdPersonCamera.transform.position = target.Find("Camera Pivot").position; Debug.Log( " 3 targets: " + targets );
            thirdPersonCamera.transform.rotation = target.Find("Camera Pivot").rotation; Debug.Log(" 4 thirdPersonCamera.transform.rotation :" + thirdPersonCamera);
        }
    }
}
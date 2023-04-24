using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchView : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cineCam;
    [SerializeField] private List<Transform> targets; //Liste des Transform que l'on veut suivre
    public Camera thirdPersonCamera;
    public GameObject[] targets;
    private int currentTargetIndex;

    public int currentInt;
    // Start is called before the first frame update
    void Start()
    {
        cineCam = GetComponent<CinemachineVirtualCamera>();
        targets = GameObject.FindGameObjectsWithTag("NPC");
        currentTargetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { // appui sur la touche espace
            Debug.Log("switch target");
            currentInt++;
            if (currentInt >= targets.Count)
            {
                currentInt = 0;
            }
            cineCam.Follow = targets[currentInt]; // change le paramtre Follow pour un nouveau Transform pris dans la liste
            
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
                Transform target = targets[currentTargetIndex].transform;
                thirdPersonCamera.transform.position = target.Find("Camera Pivot").position;
                thirdPersonCamera.transform.rotation = target.Find("Camera Pivot").rotation;
            
        }
    }

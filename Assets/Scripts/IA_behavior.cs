using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using System.Diagnostics;
using static Unity.Burst.Intrinsics.X86;
using static UnityEngine.GraphicsBuffer;
using System.Security.Cryptography;
using Unity.VisualScripting;

public class IA_behavior : MonoBehaviour
{
    // ------------------ + ATTRIBUES + --------------------

    // ------------------ + Reseau de neurones + -----------
    public NeatNetwork myNetwork;
    public int inputNodes, outputNodes, hiddenNodes;
    public int MaxDistance = 5;
    public GameObject[] Food;


    //------------------ + déplacement + --------------------
    public float speed = 1f;
    private FieldOfView fow;


    // la santé
    private const int energyToReproduce = 150;
    [SerializeField] private double energy = 100;
    [SerializeField] private double vitality = 100;
    private float energyDecreaseRate = 0.5f; // taux de diminution de l'énergie par unité de distance parcourue

    // limiste de terrain :
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    //------------------ + Start & update + --------------------

    void Awake()
    {
        fow = GetComponent<FieldOfView>();
        myNetwork = new NeatNetwork(inputNodes, outputNodes, hiddenNodes);
        Food = GameObject.FindGameObjectsWithTag("Apple");
        minX = -12;
        maxX = 10;
        maxZ = -81;
        minZ = -100;
    }

    private void FixedUpdate()
    {
        float[] input = SeeTarget();
        float[] output = myNetwork.FeedForwardNetwork(input);
        /*UnityEngine.Debug.Log(input.Length + " | " + input[0] + " | " + input[1]);*/
        UnityEngine.Debug.Log(output[0]+" | " + output[1]);

        if (output.Length != 0)
        {
            Move(output);
        }

        SeeTarget();
    }

    // ----------------- + FONTCTIONS + ------------------------


    float[] SeeTarget()
    {
        float[] input = new float[2];
        input[0] = 1; // distance par défaut à 1
        input[1] = 0; // angle par défaut à 0
        if (fow.visibleTargets.Count != 0 && fow.visibleTargets[0] != null )
        {
            float dstToTarget = Vector3.Distance(transform.position, fow.visibleTargets[0].position);
            float AngleToTarget = Vector3.Angle(transform.position, fow.visibleTargets[0].position);
            float NormalisedTarget = dstToTarget / 10f;
            float NormalisedAngle = AngleToTarget / 10f;
            input[0] = NormalisedTarget;
            input[1] = NormalisedAngle;
        }
        return input;
    }

    public void Move(float[] outputs)
    {
        // Récupérer les sorties du réseau de neurones
        float moveX = outputs[0];
        float moveZ = outputs[1];

        // Appliquer les sorties pour faire bouger l'agent
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        movement = movement.normalized * Time.deltaTime * speed; // speed est une variable qui définit la vitesse de déplacement
        transform.position = transform.position + movement;

        // Assurer que l'agent reste dans les limites du terrain
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );
    }


}

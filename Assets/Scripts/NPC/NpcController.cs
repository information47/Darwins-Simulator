using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;
using Debug = UnityEngine.Debug;
using System;
using Random = UnityEngine.Random;

public class NpcController : MonoBehaviour
{
    // [SerializeField] private GameObject Npc;
    private FieldOfView fow;

    // color
    private Renderer rend;

    // Raycast
    private float hitDivider = 1f;
    private float rayDistance = 40f;

    // movement
    private int food;
    private Vector3 lastPosition;
    private float distanceTraveled = 0f;

    // carateristiques
    [SerializeField] private double energy = 100;
    [SerializeField] private double vitality = 100;
    private float energyDecrease = 0.5f;
    private float energyThreshold = 30;
    private int energyToReproduce = 130;
    private float vitalityLoss = 0.1f;


    // network
    public NeatNetwork myNetwork;
    private int myBrainIndex;
    
    // number of input, output and hidden nodes.
    private int inputNodes;
    private int outputNodes;
    
    //inputs for neural network
    [SerializeField] private float[] sensors;

    //outputs of neural network
    [SerializeField] private float[] outputs;

    private float fitness;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        fow = GetComponent<FieldOfView>();
        
        Sensors = new float[InputNodes];

        // modify NPC size
        // this.transform.localScale = new Vector3((float)1.5, 1, (float)1.5); 

    }

    void Update()
    {
        energyLoss();

        if (vitality <= 0)
        {
            Death();
        }

        // fetch datas from sensors
        InputSensors();

        // send sensors data as input in the network
        Outputs = myNetwork.FeedForwardNetwork(Sensors);

        moveNPC(Outputs[0], Outputs[1]);

      
    }

    void moveNPC(float speed, float rotation)
    {
        // get position
        Vector3 input = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, speed * 2f), 1f);
        input = transform.TransformDirection(input);

        // move NPC
        transform.position += input * Time.deltaTime;

        // rotation of NPC
        transform.eulerAngles += new Vector3(0, (rotation * 90), 0) * Time.deltaTime;
    }
    
    void energyLoss()
    {
        //calcul de la taille du NPC
        float size = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        
        // calcul de la distance parcourue
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        //diminution de ll'énergie en fonction du temps
        energy -= energyDecrease * 2 * Time.deltaTime;

        // diminution de l'énergie en fonction de la distance parcourue et de la taille du NPC
        energy -= distanceTraveled * energyDecrease/2 * size ;
        distanceTraveled = 0f;

        if ( energy <= EnergyThreshold)
        {
            vitality -= VitalityLoss;
        }
        else if ( energy >= EnergyToReproduce)
        {
            //Reproduce();
        }

    }

    void Reproduce()
    {
        energy /= 2f; // transfert de la moitié de l'énergie au nouvel enfant

        Vector2 randomCircle = Random.insideUnitCircle * 2f; // génère une position aléatoire dans un cercle de rayon 2 autour du parent
        Vector3 childPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
        // Instantiate(Npc, childPosition, Quaternion.identity); // crée un nouvel objet NPC



    }

    private void InputSensors()
    {
        int sensorsIndex = 0;

        float[] wallDistances = Vision(transform.position);
        for (int i = 0; i < wallDistances.Length; i++)
        {
            sensors[sensorsIndex] = wallDistances[i];
            sensorsIndex++;
        }


        float dstToTarget;
        float angleToTarget;
        // if there is at least 1 visible target in the fow
        if (fow.visibleTargets.Count != 0 && fow.visibleTargets[0] != null)
        {

            Transform closetTarget = fow.SeeClosetTarget();

            // direction to the target
            Vector3 dirToTarget = (closetTarget.position - transform.position).normalized;

            // distance between target and NPC
            dstToTarget = Vector3.Distance(transform.position, closetTarget.position);

            // angle between target and NPC
            angleToTarget = Vector3.Angle(dirToTarget, transform.forward);

            Sensors[sensorsIndex] = dstToTarget;
            sensorsIndex++;

            Sensors[sensorsIndex] = angleToTarget / 10;

        }

        
    }

    public float[] Vision(Vector3 position)
    {
        float[] distances = new float[3];

        Ray r = new Ray(position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                distances[0] = hit.distance / hitDivider;
                Debug.DrawLine(r.origin, hit.point, Color.white);
            }
        }
        r.direction = (transform.forward + transform.right);
        if (Physics.Raycast(r, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                distances[1] = hit.distance / hitDivider;
                Debug.DrawLine(r.origin, hit.point, Color.white);
            }
        }
        r.direction = (transform.forward - transform.right);
        if (Physics.Raycast(r, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                distances[2] = hit.distance / hitDivider;
                Debug.DrawLine(r.origin, hit.point, Color.white);
            }
        }
        return distances;
    }

    private void Death()
    {
        GameObject.FindObjectOfType<NPCManager>().Death(fitness, MyBrainIndex);
        Destroy(gameObject);
    }

    private void ResetSensors()
    {
        for (int i = 0; i < InputNodes; i++)
        {
            Sensors[i] = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Food"))
        {
            this.Food += 1;
            energy += 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            // fitness = 0;
            Death();
        }
    }

    // getters and setters
    public int MyBrainIndex { get => myBrainIndex; set => myBrainIndex = value; }
    public float[] Sensors { get => sensors; set => sensors = value; }
    public int InputNodes { get => inputNodes; set => inputNodes = value; }
    public int OutputNodes { get => outputNodes; set => outputNodes = value; }
    public float[] Outputs { get => outputs; set => outputs = value; }
    public float EnergyThreshold { get => energyThreshold; set => energyThreshold = value; }
    public int EnergyToReproduce { get => energyToReproduce; set => energyToReproduce = value; }
    public float VitalityLoss { get => vitalityLoss; set => vitalityLoss = value; }
    public int Food { get => food; set => food = value; }
}

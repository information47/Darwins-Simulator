using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;
using Debug = UnityEngine.Debug;

public class NpcScript : MonoBehaviour
{
    [SerializeField] private GameObject Npc;

    // comportement
    private FieldOfView fow;

    // RayCast
    private float hitDivider = 1f;
    private float rayDistance = 5f;

    private bool See = false;

    private const int energyToReproduce = 150;
    
    private float[] sensors;

    private float surviveTime = 0;


    // color
    private Renderer rend;

    // movement

    public Vector3 target;
    private int food;
    private Vector3 lastPosition;
    private float distanceTraveled = 0f;

    [Range(-1f, 1f)]
    public float a, t;

    // carateristiques
    [SerializeField] private double energy = 100;
    [SerializeField] private double vitality = 100;
    private float energyDecreaseRate = 0.5f; // taux de diminution de l'énergie par unité de distance parcourue

    // network
    public NeatNetwork myNetwork;

    private int myBrainIndex;

    public int inputNodes = 5;
    public int outputNodes = 2;
    public int hiddenNodes = 0;

    public int MyBrainIndex { get => myBrainIndex; set => myBrainIndex = value; }

    private void Start()
    {
        myNetwork = new NeatNetwork(inputNodes, outputNodes, hiddenNodes);
        rend = GetComponent<Renderer>();
        fow = GetComponent<FieldOfView>();
        // this.transform.localScale = new Vector3((float)1.5, 1, (float)1.5); // modifie la taille du NPC

        sensors = new float[inputNodes];


    }
    // Update is called once per frame
    void Update()
    {
        energyLoss();

        switch (energy)
        {
            case >= energyToReproduce:
                Reproduce();
                break;

            case <= 30:
                vitality -= 0.1;
                break;

        }

        if (vitality <= 0)
        {
            Destroy(this.gameObject);
        }

        // fetch datas from sensors
        InputSensors();

        // send sensors data as input in the network
        float[] outputs = myNetwork.FeedForwardNetwork(sensors);

        moveNPC(outputs[0], outputs[1]);

      
    }

    // methodes


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



        // diminution de l'énergie en fonction de la distance parcourue et de la taille du NPC
        energy -= distanceTraveled * energyDecreaseRate * size ;
        distanceTraveled = 0f;
    }

    void Reproduce()
    {
        energy /= 2f; // transfert de la moitié de l'énergie au nouvel enfant

        Vector2 randomCircle = Random.insideUnitCircle * 2f; // génère une position aléatoire dans un cercle de rayon 2 autour du parent
        Vector3 childPosition = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
        Instantiate(Npc, childPosition, Quaternion.identity); // crée un nouvel objet NPC
    }


    
    // Champs de vision
    public Transform SeeClosetTarget()
    {
        Transform closetTraget = fow.visibleTargets[0]; 
        See = true;

        return closetTraget;
        
    }

    private void InputSensors()
    {
        float dstToTarget = 0;
        float angleToTarget = 0;

        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                sensors[0] = hit.distance / hitDivider;
                Debug.DrawLine(r.origin, hit.point, Color.white);
            }
        }
        r.direction = (transform.forward + transform.right);
        if (Physics.Raycast(r, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                sensors[1] = hit.distance / hitDivider;
                Debug.DrawLine(r.origin, hit.point, Color.white);
            }
        }
        r.direction = (transform.forward - transform.right);
        if (Physics.Raycast(r, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                sensors[2] = hit.distance / hitDivider;
                Debug.DrawLine(r.origin, hit.point, Color.white);
            }
        }

        // if there is at least 1 visible target in the fow
        if (fow.visibleTargets.Count != 0 && fow.visibleTargets[0] != null)
        {
            Transform closetTarget = SeeClosetTarget();
            Vector3 dirToTarget = (closetTarget.position - transform.position).normalized;

            // distance entre la target et le NPC
            dstToTarget = Vector3.Distance(transform.position, closetTarget.position);

            // angle entre la target et le NPC
            angleToTarget = Vector3.Angle(dirToTarget, transform.forward);


            sensors[3] = dstToTarget * 10;
            sensors[4] = angleToTarget * 10;
        }

        //r.direction = (transform.forward);
        //if (Physics.Raycast(r, out hit, rayDistance))
        //{
        //    if (hit.transform.CompareTag("Food"))
        //    {
        //        sensors[3] = hit.distance / hitDivider;
        //        Debug.DrawLine(r.origin, hit.point, Color.yellow);
        //    }
        //}
        //r.direction = (transform.forward + transform.right);
        //if (Physics.Raycast(r, out hit, rayDistance))
        //{
        //    if (hit.transform.CompareTag("Food"))
        //    {
        //        sensors[4] = hit.distance / hitDivider;
        //        Debug.DrawLine(r.origin, hit.point, Color.yellow);
        //    }
        //}
        //r.direction = (transform.forward - transform.right);
        //if (Physics.Raycast(r, out hit, rayDistance))
        //{
        //    if (hit.transform.CompareTag("Food"))
        //    {
        //        sensors[5] = hit.distance / hitDivider;
        //        Debug.DrawLine(r.origin, hit.point, Color.yellow);
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Food"))
        {
            this.food += 1;
            energy += 10;
        } else if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    
}

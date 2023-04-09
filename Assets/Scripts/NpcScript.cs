using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;

public class NpcScript : MonoBehaviour
{
    [SerializeField] private GameObject Npc;

    //comportement
    private FieldOfView fow;

    float objectiveX = 0f;
    float objectiveY = 0f;
    float objectiveZ = 0f;

    private bool See = false;

    private const int energyToReproduce = 150;

    //couleur
    private Renderer rend;

    //déplacement
    NavMeshAgent agent;
    public Transform[] waypoint;
    float waypointIndexX;
    float waypointIndexZ;
    Vector3 target;
    private int food;
    private bool satiated = false;
    private Vector3 lastPosition;
    private float distanceTraveled = 0f;

    // carateristiques
    [SerializeField] private double energy = 100;
    [SerializeField] private double vitality = 100;
    private float energyDecreaseRate = 1.0f; // taux de diminution de l'énergie par unité de distance parcourue



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        rend = GetComponent<Renderer>();
        fow = GetComponent<FieldOfView>();
        // agent.speed = agent.speed * 5;   // modifie la vitesse du NPC
        // this.transform.localScale = new Vector3((float)1.5, 1, (float)1.5); // modifie la taille du NPC
    }
    // Update is called once per frame
    void Update()
    {
        energyLoss();
        
        switch(energy)
        {
            case  >= energyToReproduce:
                Reproduce();
                break;

            case <= 30:
                vitality -= 0.1;
                break;

        }
        
        if (vitality <=0)
        {
            Destroy(this.gameObject);
        }


        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
        }
        SeeTarget();
        if (See)
        {
            GoEat(fow.visibleTargets);
            See = false;
        }
        else
        {
            UpdateDestination();
            if (satiated == true)
            {
                rend.material.color = Color.cyan;
            }
            UpdateDestination();
        }
    }

    // methodes
    // déplacements
    void UpdateDestination()
    {
        target = new Vector3(waypointIndexX, transform.position.y, waypointIndexZ);
        agent.SetDestination(target);
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


    void IterateWaypointIndex()
    {
        float minX = 0;
        float maxX = 0;
        float minZ = 0;
        float maxZ = 0;
        foreach (var waypoint in waypoint)
        {
            if (waypoint.transform.position.x < minX)
            {
                minX = waypoint.transform.position.x;
            }
            if (waypoint.transform.position.x > maxX)
            {
                maxX = waypoint.transform.position.x;
            }
            if (waypoint.transform.position.z < minZ)
            {
                minZ = waypoint.transform.position.z;
            }
            if (waypoint.transform.position.z > maxZ)
            {
                maxZ = waypoint.transform.position.z;
            }
        }

        waypointIndexX = Random.Range(minX, maxX);
        waypointIndexZ = Random.Range(minZ, maxZ);
    }
    // Champs de vision
    void SeeTarget()
    {
        if (fow.visibleTargets.Count != 0 && fow.visibleTargets[0] != null)
        {
            objectiveX = fow.visibleTargets[0].position.x;
            objectiveY = fow.visibleTargets[0].position.y;
            objectiveZ = fow.visibleTargets[0].position.z;

            See = true;
        }
    }

    void GoEat(List<Transform> visibleTargets)
    {
        target = new Vector3(objectiveX, objectiveY, objectiveZ);
        agent.SetDestination(target);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Food"))
        {
            this.food += 1;
            energy += 10;

            if (this.food == 2)
            {
                satiated = true;
            }

        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NpcScript : MonoBehaviour
{

    //comportement
    private FieldOfView fow;
<<<<<<< HEAD
  
=======
    float objectiveX = 0f;
    float objectiveY = 0f;
    float objectiveZ = 0f;
>>>>>>> a2d766cd4c56f9070c33a1477db8d8a215983891
    private bool See = false;

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

    // carateristiques
    public double energy = 1000;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        rend = GetComponent<Renderer>();
        fow = GetComponent<FieldOfView>();
    }
    // Update is called once per frame
    void Update()
    {
        energy -= 0.01;
        if (energy <= 0)
        {
            Destroy(this.gameObject);
        }
        


        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
<<<<<<< HEAD
            SeeTarget();
            if (See)
            {
               // GoEat(fow.visibleTargets);
            }
=======
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
>>>>>>> a2d766cd4c56f9070c33a1477db8d8a215983891
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
<<<<<<< HEAD
        if (fow.visibleTargets.Count != 0)
        {
=======
        if (fow.visibleTargets.Count != 0 && fow.visibleTargets[0] != null)
        {
            objectiveX = fow.visibleTargets[0].position.x;
            objectiveY = fow.visibleTargets[0].position.y;
            objectiveZ = fow.visibleTargets[0].position.z;
>>>>>>> a2d766cd4c56f9070c33a1477db8d8a215983891
            See = true;
        }
    }

    void GoEat(List<Transform> visibleTargets)
    {
<<<<<<< HEAD
        target = new Vector3(visibleTargets[0].position.x, visibleTargets[0].position.y, visibleTargets[0].position.z);
=======
        target = new Vector3(objectiveX, objectiveY, objectiveZ);
        agent.SetDestination(target);
>>>>>>> a2d766cd4c56f9070c33a1477db8d8a215983891
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Food"))
        {
            this.food += 1;
            if (this.food == 2)
            {
                satiated = true;
                energy += 100;
            }

        }
    }

    
}

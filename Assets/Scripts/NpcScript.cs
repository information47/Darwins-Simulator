using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NpcScript : MonoBehaviour
{

    //comportement
    private FieldOfView fow;
  
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
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            SeeTarget();
            if (See)
            {
               // GoEat(fow.visibleTargets);
            }
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
        if (fow.visibleTargets.Count != 0)
        {
            See = true;
        }
    }

    void GoEat(List<Transform> visibleTargets)
    {
        target = new Vector3(visibleTargets[0].position.x, visibleTargets[0].position.y, visibleTargets[0].position.z);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Food"))
        {
            this.food += 1;
            if (this.food == 2)
            {
                satiated = true;
            }

        }
    }

    
}

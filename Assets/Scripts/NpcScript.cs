using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcScript : MonoBehaviour
{
    private Renderer rend;
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
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
            if (satiated == true)
            {
                rend.material.color = Color.cyan;
            }
        }
    }

    // methodes 
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            this.food += 1;
            Debug.Log("food + 1");
            if (this.food == 2)
            {
                satiated = true;
            }

        }
    }
}

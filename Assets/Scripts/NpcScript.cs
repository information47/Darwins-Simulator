using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NpcScript : MonoBehaviour
{

    //comportement
    private FieldOfView fow;
    float objectiveX = 0f;
    float objectiveY = 0f;
    float objectiveZ = 0f;
    private bool See = false;

    //couleur
    private Renderer rend;

    //déplacement
    NavMeshAgent agent;
    public GameObject[] waypoint;
    public float waypointIndexX;
    public float waypointIndexZ;
    Vector3 target;
    private int food;
    private bool satiated = false;

    public float minX = 0;
    public float maxX = 0;
    public float minZ = 0;
    public float maxZ = 0;


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
        }
    }

    // methodes
    // déplacements
    void UpdateDestination()
    {
        target = new Vector3(waypointIndexX, this.transform.position.y, waypointIndexZ);
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypoint = GameObject.FindGameObjectsWithTag("Waypoint");

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
            if (this.food == 2)
            {
                satiated = true;
            }

        }
    }

    
}

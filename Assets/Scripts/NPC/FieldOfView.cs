using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float ViewRadius = 15;
    [Range(0, 360)]
    public float ViewAngle;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;
    public string foodTag = "food";

    public List<Transform> visibleTargets = new List<Transform>();

    private bool see = false;


    private void Start()
    {
        StartCoroutine("FindTargetWithDelay",0.2f);
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {

            yield return new WaitForSeconds (delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] TargetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, TargetMask);
        for (int i = 0; i < TargetsInViewRadius.Length; i++)
        {
            Transform target = TargetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward,dirToTarget) < ViewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                
                //si tu vois la target
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    if (target != null)
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }

    }

    public Transform SeeClosetTarget()
    {
        Transform closetTraget = visibleTargets[0];
        See = true;

        return closetTraget;

    }
    public float ClosetTargetDist()
    {
        float dstToTarget;
        // if there is at least 1 visible target in the fow
        if (visibleTargets.Count != 0 && visibleTargets[0] != null)
        {

            Transform closetTarget = SeeClosetTarget();



            // distance between target and NPC
            dstToTarget = Vector3.Distance(transform.position, closetTarget.position);



            return dstToTarget;

        }
        else { return 0; }
    }

    public float ClosetTargetAngle(float divider = 10)
    {
        float angleToTarget;
        if (visibleTargets.Count != 0 && visibleTargets[0] != null)
        {
            Transform closetTarget = SeeClosetTarget();

            // direction to the target
            Vector3 dirToTarget = (closetTarget.position - transform.position).normalized;

            // angle between target and NPC
            angleToTarget = Vector3.Angle(dirToTarget, transform.forward);

            return angleToTarget / divider;
        }
        else { return 0f; }
    }

    public Vector3  DirFromAngle( float AngleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            AngleDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(AngleDegrees * Mathf.Deg2Rad),0,Mathf.Cos(AngleDegrees * Mathf.Deg2Rad));
    }

    // getters and setters
    public bool See { get => see; set => see = value; }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{


    public float ViewRadius;
    [Range(0, 360)]
    public float ViewAngle;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

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

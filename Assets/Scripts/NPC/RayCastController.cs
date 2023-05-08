using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayCastController : MonoBehaviour
{
    private float hitDivider = 1f;
    private float rayDistance = 40f;

    public float sendRay(Vector3 position, Vector3 direction)
    {
        float dist;
        Ray r = new(position, direction);
        RaycastHit hit;


        if (Physics.Raycast(r, out hit, rayDistance) && hit.transform.CompareTag("Wall"))
        {
                Debug.DrawLine(r.origin, hit.point, Color.white);
                dist = hit.distance / hitDivider;
                return dist;
        }
        else { return rayDistance; }
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


}

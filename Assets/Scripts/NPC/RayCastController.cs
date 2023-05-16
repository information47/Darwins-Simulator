using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.UIElements;

public class RayCastController
{
    public float RayHit(Vector3 origin, Vector3 direction, string tag, float maxDistance, float hitDivider)
    {
        float dist = 0f;
        Ray r = new(origin, direction);
        RaycastHit hit;


        if (Physics.Raycast(r, out hit, maxDistance) && hit.transform.CompareTag(tag))
        {
                Debug.DrawLine(r.origin, hit.point, Color.white);
                dist = hit.distance / hitDivider;
        }
        return dist;
    }

    //public float[] vision(vector3 position, float distance)
    //{
    //    float[] distances = new float[3];

    //    ray r = new ray(position, transform.forward);
    //    raycasthit hit;
    //    if (physics.raycast(r, out hit, distance))
    //    {
    //        if (hit.transform.comparetag("wall"))
    //        {
    //            distances[0] = hit.distance / hitdivider;
    //            debug.drawline(r.origin, hit.point, color.white);
    //        }
    //    }
    //    r.direction = (transform.forward + transform.right);
    //    if (physics.raycast(r, out hit, distance))
    //    {
    //        if (hit.transform.comparetag("wall"))
    //        {
    //            distances[1] = hit.distance / hitdivider;
    //            debug.drawline(r.origin, hit.point, color.white);
    //        }
    //    }
    //    r.direction = (transform.forward - transform.right);
    //    if (physics.raycast(r, out hit, distance))
    //    {
    //        if (hit.transform.comparetag("wall"))
    //        {
    //            distances[2] = hit.distance / hitdivider;
    //            debug.drawline(r.origin, hit.point, color.white);
    //        }
    //    }
    //    return distances;
    //}


}

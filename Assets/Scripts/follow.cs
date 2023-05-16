
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform target;

    public float smoothness = 0.125f;
    public Vector3 offstet;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offstet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}

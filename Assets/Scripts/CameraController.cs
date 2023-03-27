using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 10.0f; // vitesse de d�placement de la cam�ra
    public float sensitivity = 3.0f; // sensibilit� de la souris

    private float mouseX, mouseY;

    void Update()
    {
        // D�placement de la cam�ra
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        // Rotation de la cam�ra avec la souris
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}

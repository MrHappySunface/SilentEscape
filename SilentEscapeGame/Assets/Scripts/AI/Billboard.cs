using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        // Get the camera position
        Vector3 cameraPosition = Camera.main.transform.position;

        // Make the monster face the camera, but only on the Y-axis
        Vector3 direction = cameraPosition - transform.position;
        direction.y = -90f;  // Ignore the vertical component

        // Apply the rotation
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}

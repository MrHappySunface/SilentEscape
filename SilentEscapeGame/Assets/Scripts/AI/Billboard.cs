using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = Quaternion.LookRotation(Camera.main.transform.position).eulerAngles;
        rotation.y = 0f;

        transform.rotation = Quaternion.Euler(rotation);
        transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward);
    }
}

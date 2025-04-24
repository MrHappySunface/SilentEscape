using UnityEngine;

public class JumpscareFace : MonoBehaviour
{
    public float retreatSpeed = 1f;
    public float scareDuration = 2f;
    public float lingerTime = 1f;  // Time to linger before retreating

    private float timer;
    private bool isRetreating = false;

    void Start()
    {
        timer = scareDuration;
    }

    void Update()
    {
        if (lingerTime > 0f)
        {
            // Linger in front of the camera for 'lingerTime' seconds
            lingerTime -= Time.deltaTime;
        }
        else if (!isRetreating)
        {
            // After linger time, start retreating
            isRetreating = true;
        }

        if (isRetreating)
        {
            // Move backward in local space after lingering
            transform.localPosition += Vector3.forward * retreatSpeed * Time.deltaTime;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

// Sound based navigation
/*
using UnityEngine;  // Core Unity functions
using System.Collections;  // Required for coroutines

public class SonarSystem : MonoBehaviour
{
    public float sonarSpeed = 5f;
    public float sonarRadius = 5f;

    void Start()
    {
        StartCoroutine(ExpandSonar());
    }

    IEnumerator ExpandSonar()
    {
        float currentRadius = 0f;
        while (currentRadius < sonarRadius)
        {
            currentRadius += sonarSpeed * Time.deltaTime;
            transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);
            yield return null;
        }
        Destroy(gameObject);
    }
}

*/
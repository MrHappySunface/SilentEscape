using UnityEngine;
using System.Collections;

public class SonarLightSpawner : MonoBehaviour
{
    public GameObject sonarLightPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 20f;
    public float yHeight = 1.5f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3 randomPos = GetRandomSpawnPosition();
            GameObject light = Instantiate(sonarLightPrefab, randomPos, Quaternion.identity);

            // 👹 Alert monster
            if (MonsterAI.Instance != null)
                MonsterAI.Instance.AlertMonster(randomPos);

            Debug.Log("🔆 Sonar Light spawned at " + randomPos);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0;
        return transform.position + randomOffset + Vector3.up * yHeight;
    }
}

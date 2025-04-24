using UnityEngine;

public class JumpscareManager : MonoBehaviour
{
    public GameObject jumpscarePrefab;
    public float faceDistance = 0.2f;
    public float scareDuration = 2f;
    public float cooldownTime = 5f;

    private bool hasScared = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (hasScared)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                hasScared = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasScared && other.CompareTag("MainCamera"))
        {
            hasScared = true;
            cooldownTimer = cooldownTime;

            GameObject face = Instantiate(jumpscarePrefab);
            Transform cam = other.transform;

            face.transform.SetParent(cam);
            face.transform.localPosition = new Vector3(0, -1f, faceDistance);
            face.transform.localRotation = Quaternion.identity;

            face.GetComponent<AudioSource>()?.Play();

            var faceScript = face.GetComponent<JumpscareFace>();
            if (faceScript != null)
            {
                faceScript.scareDuration = scareDuration;
                faceScript.lingerTime = 1f;
            }

            // Teleport the monster away after the jumpscare is triggered
            if (MonsterAI.Instance != null)
            {
                // You can set a specific position, or make it random
                Vector3 teleportPosition = new Vector3(Random.Range(-30f, 30f), 0, Random.Range(-30f, 30f));
                MonsterAI.Instance.TeleportMonster(teleportPosition);
            }
        }
    }


}

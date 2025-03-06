// Player movement and interaction
/*
using UnityEngine;  // Core Unity functionalities
using UnityEngine.XR;  // VR input detection
using UnityEngine.XR.Interaction.Toolkit;  // VR hand interactions

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject sonarWavePrefab;
    public Transform waveSpawnPoint;
    public float sonarCooldown = 2.0f;
    private float lastSonarTime;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetButtonDown("Fire1") && Time.time > lastSonarTime + sonarCooldown)
        {
            EmitSonarWave();
            lastSonarTime = Time.time;
        }
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }

    void EmitSonarWave()
    {
        Instantiate(sonarWavePrefab, waveSpawnPoint.position, Quaternion.identity);
    }
}

*/
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class VRSonarTrigger : MonoBehaviour
{
    [Header("Sonar Settings")]
    public float sonarRadius = 15f;
    public float sonarIntensity = 1f;
    public float sonarCooldown = 3f;
    public GameObject sonarWavePrefab;
    public Transform sonarOriginTransform; // Assign right/left hand here

    [Header("Trigger Type")]
    public bool useControllerInput = true;
    public bool useHeadMovement = false;

    private float cooldownTimer = 0f;
    private InputDevice leftHand;
    private InputDevice rightHand;

    private Quaternion previousHeadRotation;
    public float headTurnThreshold = 60f; // degrees per second

    void Start()
    {
        RefreshDevices();

        // Initialize previous head rotation for head turn detection
        var headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        if (headDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rot))
        {
            previousHeadRotation = rot;
        }
    }

    void Update()
    {
        // Refresh devices if invalid (e.g., after sleep)
        if (!leftHand.isValid || !rightHand.isValid)
            RefreshDevices();

        cooldownTimer -= Time.deltaTime;
        bool sonarTriggered = false;

        if (cooldownTimer <= 0f)
        {
            if (useControllerInput && (IsPrimaryButtonPressed(leftHand) || IsPrimaryButtonPressed(rightHand)))
            {
                sonarTriggered = true;
            }

            if (useHeadMovement && DetectQuickHeadTurn())
            {
                sonarTriggered = true;
            }

            // Fallback for editor or desktop testing
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Simulated sonar triggered with G key.");
                sonarTriggered = true;
            }

            if (sonarTriggered)
            {
                EmitSonar();
            }
        }
    }

    private void RefreshDevices()
    {
        var leftDevices = new List<InputDevice>();
        var rightDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightDevices);

        if (leftDevices.Count > 0) leftHand = leftDevices[0];
        if (rightDevices.Count > 0) rightHand = rightDevices[0];

        if (!leftHand.isValid) Debug.LogWarning("❌ Left hand controller not found.");
        if (!rightHand.isValid) Debug.LogWarning("❌ Right hand controller not found.");
    }

    private bool IsPrimaryButtonPressed(InputDevice device)
    {
        if (device.isValid && device.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed))
        {
            if (pressed)
                Debug.Log($"✅ {device.name} primary button pressed.");
            return pressed;
        }
        return false;
    }

    void EmitSonar()
    {
        Vector3 origin = sonarOriginTransform ? sonarOriginTransform.position : transform.position;

        Collider[] hits = Physics.OverlapSphere(origin, sonarRadius);
        foreach (var hit in hits)
        {
            var detector = hit.GetComponent<MonsterSoundDetection>();
            if (detector != null)
            {
                detector.DetectSound(origin, sonarIntensity);
            }

            var glow = hit.GetComponent<SonarInteractable>();
            if (glow != null)
            {
                glow.TriggerGlow(1.5f);
            }
        }

        if (sonarWavePrefab != null)
        {
            GameObject wave = Instantiate(sonarWavePrefab, origin, Quaternion.identity);
            Destroy(wave, 3f);
        }

        Debug.Log("🎯 Sonar Pulse Emitted from: " + origin);
        cooldownTimer = sonarCooldown;
    }

    private bool DetectQuickHeadTurn()
    {
        var headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        if (headDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion currentRot))
        {
            float angleDelta = Quaternion.Angle(previousHeadRotation, currentRot);
            previousHeadRotation = currentRot;
            return angleDelta / Time.deltaTime > headTurnThreshold;
        }
        return false;
    }
}

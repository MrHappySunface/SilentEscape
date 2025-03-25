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

    [Header("Trigger Type")]
    public bool useControllerInput = true;
    public bool useHeadMovement = false;

    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            if (useControllerInput && IsTriggerPressed())
            {
                EmitSonar();
            }

            if (useHeadMovement && DetectQuickHeadTurn())
            {
                EmitSonar();
            }
        }
    }

    private bool IsTriggerPressed()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool pressed) && pressed)
            {
                return true;
            }
        }

        return false;
    }

    void EmitSonar()
    {
        Vector3 origin = transform.position;

        Collider[] hits = Physics.OverlapSphere(origin, sonarRadius);
        foreach (var hit in hits)
        {
            var detector = hit.GetComponent<MonsterSoundDetection>();
            if (detector != null)
            {
                detector.DetectSound(origin, sonarIntensity);
            }
        }

        if (sonarWavePrefab != null)
        {
            Instantiate(sonarWavePrefab, origin, Quaternion.identity);
        }

        Debug.Log("🎯 Sonar Pulse Emitted from VR Player");
        cooldownTimer = sonarCooldown;
    }

    private bool DetectQuickHeadTurn()
    {
        if (InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rot))
        {
            Vector3 headEuler = rot.eulerAngles;
            return Mathf.Abs(headEuler.y) > 60f;
        }
        return false;
    }
}

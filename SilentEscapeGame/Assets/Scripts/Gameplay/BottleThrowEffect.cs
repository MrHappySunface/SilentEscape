using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BottleThrowEffect : MonoBehaviour
{
    public GameObject sonarLightPrefab;

    private bool hasBeenReleased = false;

    private void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        hasBeenReleased = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenReleased)
        {
            if (sonarLightPrefab != null)
            {
                Instantiate(sonarLightPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}

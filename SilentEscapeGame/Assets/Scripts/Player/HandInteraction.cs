// VR Hand Interactions

using UnityEngine;  // Unity base library
using UnityEngine.XR;  // XR Input detection
using UnityEngine.XR.Interaction.Toolkit;  // VR interaction toolkit


public class HandInteraction : MonoBehaviour
{
    public XRController leftHand;
    public XRController rightHand;
    public LayerMask interactableLayer;

    void Update()
    {
        HandleInteraction(leftHand);
        HandleInteraction(rightHand);
    }

    void HandleInteraction(XRController hand)
    {
        if (hand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            Ray ray = new Ray(hand.transform.position, hand.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 2f, interactableLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
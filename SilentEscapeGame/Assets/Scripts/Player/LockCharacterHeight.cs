using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LockCharacterHeight : MonoBehaviour
{
    [Tooltip("The height you want to force (in meters)")]
    public float fixedHeight = 0.5f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController != null)
        {
            characterController.height = fixedHeight;
            characterController.center = new Vector3(0, fixedHeight / 2f, 0);
            Debug.Log("🔒 Character height locked to " + fixedHeight);
        }
    }

    void LateUpdate()
    {
        // Re-apply height every frame just in case something tries to change it
        if (characterController.height != fixedHeight)
        {
            characterController.height = fixedHeight;
            characterController.center = new Vector3(0, fixedHeight / 2f, 0);
        }
    }
}
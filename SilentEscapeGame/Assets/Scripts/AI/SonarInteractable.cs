using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SonarInteractable : MonoBehaviour
{
    public Material glowMaterial; // Outline material (should use sharedMaterial)

    private Renderer rend;
    private bool isGlowing = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void TriggerGlow(float duration)
    {
        if (!isGlowing)
        {
            AddGlowMaterial();
            StartCoroutine(RemoveGlowAfterDelay(duration));
        }
        else
        {
            StopAllCoroutines(); // Reset timer
            StartCoroutine(RemoveGlowAfterDelay(duration));
        }
    }

    private void AddGlowMaterial()
    {
        var currentMats = new List<Material>(rend.sharedMaterials);

        // Only add if not already there (by shader name or comparing the actual reference)
        if (!currentMats.Contains(glowMaterial))
        {
            currentMats.Add(glowMaterial);
            rend.materials = currentMats.ToArray(); // Use .materials to trigger the change
            isGlowing = true;
        }
    }

    private IEnumerator RemoveGlowAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveGlowMaterial();
    }

    private void RemoveGlowMaterial()
    {
        var currentMats = new List<Material>(rend.sharedMaterials);
        if (currentMats.Contains(glowMaterial))
        {
            currentMats.Remove(glowMaterial);
            rend.materials = currentMats.ToArray(); // Apply change with .materials
        }

        isGlowing = false;
    }
}

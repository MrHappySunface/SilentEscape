using UnityEngine;

public class SonarInteractable : MonoBehaviour
{
    public Material glowMaterial;
    private Material originalMat;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMat = rend.material;
    }

    public void TriggerGlow(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(GlowRoutine(duration));
    }

    private System.Collections.IEnumerator GlowRoutine(float duration)
    {
        rend.material = glowMaterial;
        yield return new WaitForSeconds(duration);
        rend.material = originalMat;
    }
}

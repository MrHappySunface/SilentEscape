using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    public bool isSolved = false;
    public Light statusLight;
    public AudioClip solvedSound;

    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetLight(false);
    }

    public virtual void SolvePuzzle()
    {
        if (isSolved) return;

        isSolved = true;
        SetLight(true);
        PlaySolvedFeedback();
        PuzzleManager.Instance?.PuzzleCompleted(this);
    }

    void SetLight(bool solved)
    {
        if (statusLight)
            statusLight.color = solved ? Color.green : Color.red;
    }

    void PlaySolvedFeedback()
    {
        if (audioSource && solvedSound)
            audioSource.PlayOneShot(solvedSound);
    }
}

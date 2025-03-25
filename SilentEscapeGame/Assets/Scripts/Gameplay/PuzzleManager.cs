using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public List<PuzzleBase> puzzles = new List<PuzzleBase>();
    public GameObject exitDoor;

    void Awake() => Instance = this;

    public void PuzzleCompleted(PuzzleBase puzzle)
    {
        if (AllSolved()) UnlockExit();
    }

    private bool AllSolved()
    {
        foreach (var puzzle in puzzles)
            if (!puzzle.isSolved) return false;
        return true;
    }

    private void UnlockExit()
    {
        Debug.Log("✅ All puzzles solved. Unlocking exit!");
        if (exitDoor != null) exitDoor.SetActive(false);
    }
}

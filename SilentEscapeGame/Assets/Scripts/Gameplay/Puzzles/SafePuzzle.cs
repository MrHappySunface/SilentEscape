using UnityEngine;

public class SafePuzzle : PuzzleBase
{
    public string correctCode = "427";
    private string enteredCode = "";

    public void EnterDigit(string digit)
    {
        if (isSolved) return;

        enteredCode += digit;
        if (enteredCode.Length >= correctCode.Length)
        {
            if (enteredCode == correctCode)
            {
                SolvePuzzle();
            }
            else
            {
                enteredCode = "";
                Debug.Log("❌ Incorrect Code. Try again.");
            }
        }
    }
}

// LockCode to unlock the vault/door

using UnityEngine;  // Core Unity functions
using UnityEngine.UI;  // Handles UI elements
using UnityEngine.Audio;  // Plays sound effects

public class SafeLock : MonoBehaviour
{
    public string correctCode = "3389";
    private string enteredCode = "";
    public GameObject safeDoor;
    public AudioSource unlockSound;
    public Text displayText;

    public void EnterDigit(string digit)
    {
        if (enteredCode.Length < correctCode.Length)
        {
            enteredCode += digit;
            displayText.text = enteredCode;
        }
    }

    public void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            UnlockSafe();
        }
        else
        {
            enteredCode = "";
            displayText.text = "Incorrect";
        }
    }

    private void UnlockSafe()
    {
        safeDoor.transform.Rotate(0, 90, 0); // Simulates opening
        unlockSound.Play();
        displayText.text = "Unlocked!";
    }
}
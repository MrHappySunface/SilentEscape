// Traps, tripwires, glassess, etc..

using UnityEngine;  // Core Unity functions

public class TrapSystem : MonoBehaviour
{
    public enum TrapType { BearTrap, Tripwire, Hole }
    public TrapType trapType;
    public AudioSource trapSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trapType == TrapType.BearTrap)
            {
                Debug.Log("Player Trapped!");
                trapSound.Play();
                StartCoroutine(TrapDuration(other.gameObject));
            }
            else if (trapType == TrapType.Hole)
            {
                Debug.Log("Player Fell!");
                other.GetComponent<PlayerController>().enabled = false;
            }
        }
    }

    IEnumerator TrapDuration(GameObject player)
    {
        player.GetComponent<PlayerController>().speed = 0;
        yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerController>().speed = 3.0f;
    }
}
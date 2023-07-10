using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnTrigger : MonoBehaviour
{
    public Transform defaultSpawnPoint; // Reference to the default spawn point of the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        player.transform.position = defaultSpawnPoint.position;
        player.transform.rotation = defaultSpawnPoint.rotation;
        // Add any additional logic for respawning, like resetting health, etc.
    }
}



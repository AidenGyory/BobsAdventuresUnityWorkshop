using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 currentCheckpoint;
    public static bool hasCheckpointInScene = false;

    private void Start()
    {
        currentCheckpoint = PlayerController.instance.transform.position;
        hasCheckpointInScene = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentCheckpoint = transform.position;
        }
    }
}

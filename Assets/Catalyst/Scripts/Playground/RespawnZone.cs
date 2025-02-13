using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    Vector3 defaultPos;
    private void Start()
    {
        defaultPos = PlayerController.instance.transform.position;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.transform.SetParent(null);
            PlayerController.instance.transform.localScale = Vector3.one;
            PlayerController.instance.ChangePosition(Checkpoint.hasCheckpointInScene ? Checkpoint.currentCheckpoint : defaultPos);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float xDirection = 0f;
    public float yDirection = 0f;
    public float zDirection = 0f;
    public float speed = 1f;
    public float pauseAmount = 1f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private GameObject player; // Reference to the player object

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(xDirection, yDirection, zDirection);
        StartMovement();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        // Calculate the new position based on the direction, speed, and time
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Check if the object has reached the target position
        if (transform.position == targetPosition)
        {
            isMoving = false;
            Invoke("StartMovement", pauseAmount);
        }
    }

    private void StartMovement()
    {
        isMoving = true;
        if (targetPosition == startPosition)
        {
            targetPosition = startPosition + new Vector3(xDirection, yDirection, zDirection);
        }
        else
        {
            targetPosition = startPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.parent = transform; // Parent the player to the platform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && player == other.gameObject)
        {
            player.transform.parent = null; // Unparent the player from the platform
            player = null;
        }
    }
}






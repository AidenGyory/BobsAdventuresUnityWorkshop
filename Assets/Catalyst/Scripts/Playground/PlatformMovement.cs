using UnityEngine;
using UnityEditor;

public class PlatformMovement : Platform
{
    public float xDirection = 0f;
    public float yDirection = 0f;
    public float zDirection = 0f;
    public float speed = 1f;
    public float pauseAmount = 1f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool isMoving = false;

    [Header("Ghost Platform")]
    public GameObject ghostPlatform; // Reference to the ghost platform

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(xDirection, yDirection, zDirection);

        UpdateGhostPlatform(); // Ensure ghost platform is set up at start
        StartMovement();
    }

    private void OnValidate()
    {
        // Update target position and ghost platform when values are changed in the inspector
        targetPosition = transform.position + new Vector3(xDirection, yDirection, zDirection);
        UpdateGhostPlatform();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        // Calculate the movement step and move the platform
        float step = speed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Update the platform's position
        transform.position = newPosition;

        // Check if platform reached the target
        if (transform.position == targetPosition)
        {
            isMoving = false;
            Invoke(nameof(StartMovement), pauseAmount);
        }
    }

    private void StartMovement()
    {
        isMoving = true;

        // Swap between start and target positions
        targetPosition = (targetPosition == startPosition)
            ? startPosition + new Vector3(xDirection, yDirection, zDirection)
            : startPosition;
    }

    private void UpdateGhostPlatform()
    {
        if (ghostPlatform != null)
        {
            // Update the ghost platform's position
            ghostPlatform.transform.position = targetPosition;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (ghostPlatform != null)
        {
            DrawAnimatedDashedLine(transform.position, ghostPlatform.transform.position, 0.5f);
        }
    }

    private void DrawAnimatedDashedLine(Vector3 start, Vector3 end, float dashLength)
    {
        // Calculate the total distance and direction
        float totalDistance = Vector3.Distance(start, end);
        Vector3 direction = (end - start).normalized;

        // Animation offset using time
        float animationOffset = (Time.realtimeSinceStartup * speed / 2) % (dashLength * 4);

        // Draw the dashes
        for (float i = animationOffset; i < totalDistance; i += dashLength * 4)
        {
            Vector3 dashStart = start + direction * i;
            Vector3 dashEnd = dashStart + direction * Mathf.Min(dashLength, totalDistance - i);

            // Limit to the end point of the line
            if ((dashStart - start).magnitude > totalDistance) break;

            Gizmos.color = Color.cyan; // Customize the color
            Gizmos.DrawLine(dashStart, dashEnd);
        }
    }
}


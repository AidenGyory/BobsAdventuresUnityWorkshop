using UnityEngine;

public class SpinningPlatform : Platform
{
    private Transform groundedTransform;
    private Vector3 lastGroundedPosition;
    private Quaternion lastGroundedRotation;



    private void FixedUpdate()
    {
        if (groundedTransform != null)
        {
            // Calculate platform movement and rotation.
            Vector3 newGroundedPosition = groundedTransform.position;
            Quaternion newGroundedRotation = groundedTransform.rotation;

            // Calculate external motion from platform movement.
            Vector3 externalMotion = newGroundedPosition - lastGroundedPosition;

            // Calculate external rotation from platform rotation.
            Quaternion deltaRotation = newGroundedRotation * Quaternion.Inverse(lastGroundedRotation);
            Vector3 rotatedRight = deltaRotation * Vector3.right;
            rotatedRight.y = 0.0f; // Ignore vertical rotation.

            float externalRotation = rotatedRight.magnitude > 0.0f
                ? Vector3.SignedAngle(Vector3.right, rotatedRight.normalized, Vector3.up)
                : 0.0f;

            if (PlayerIsOnPlatform)
            {
                PlayerCharacterController.Move(externalMotion);
                PlayerTransform.RotateAround(groundedTransform.position, Vector3.up, externalRotation);
            }

            // Update last known position and rotation of the platform.
            lastGroundedPosition = newGroundedPosition;
            lastGroundedRotation = newGroundedRotation;



        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.attachedPlatform = this;
            // Track the platform the player is standing on.
            groundedTransform = transform;
            lastGroundedPosition = groundedTransform.position;
            lastGroundedRotation = groundedTransform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        groundedTransform = null;
           if (PlayerController.instance.attachedPlatform == this)
            {
                
                PlayerController.instance.attachedPlatform = null;
            }
        }
    }

}

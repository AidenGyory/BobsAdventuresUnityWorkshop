using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.attachedPlatform = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerController.instance.attachedPlatform == this)
            {
                PlayerController.instance.attachedPlatform = null;
            }
        }
    }

    public bool PlayerIsOnPlatform => PlayerController.instance.attachedPlatform == this;
    protected CharacterController PlayerCharacterController => PlayerController.instance.Controller;
    protected Transform PlayerTransform => PlayerController.instance.transform;
}

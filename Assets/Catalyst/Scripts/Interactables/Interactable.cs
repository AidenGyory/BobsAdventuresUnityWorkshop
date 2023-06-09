using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool overlapped;
    protected string prompt;

    private void Update()
    {
        if (overlapped)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        OnUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overlapped = true;
            InteractManager.instance.ShowPrompt(prompt);
            TriggerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerExit();
            InteractManager.instance.HidePrompt();
            overlapped = false;
        }
    }

    public virtual void OnUpdate() { }
    public virtual void TriggerEnter() { }
    public virtual void TriggerExit() { }

    public virtual void Interact() { }

    private void OnDisable()
    {
        overlapped = false;
    }
}

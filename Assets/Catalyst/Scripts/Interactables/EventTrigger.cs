using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerEnterEvent;
    [SerializeField] private UnityEvent OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Invoke the OnTriggerEnterEvent when an object enters the collider
        OnTriggerEnterEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        // Invoke the OnTriggerExitEvent when an object exits the collider
        OnTriggerExitEvent?.Invoke();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    public Transform cameraPoint;
    public List<DialogueLine> lines;
    bool overlapped = false;

    private void Update()
    {
        if (overlapped)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.instance.StartDialogue(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overlapped = true;
            DialogueManager.instance.uiPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.instance.uiPrompt.SetActive(false);
            overlapped = false;
        }
    }

    private void OnDisable()
    {
        overlapped = false;
    }
}

[System.Serializable]
public class DialogueLine
{
    public string name;
    public string line;
}
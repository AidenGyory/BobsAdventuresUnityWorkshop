using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : Interactable
{
    public Transform cameraPoint;
    public float cameraDistance;
    public List<DialogueLine> lines;

    private void Start()
    {
        prompt = "Talk";
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(this);
    }
}

[System.Serializable]
public class DialogueLine
{
    public string name;
    public string line;
}

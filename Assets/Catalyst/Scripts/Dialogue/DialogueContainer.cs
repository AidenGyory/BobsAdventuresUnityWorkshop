using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : Interactable
{
    public Transform cameraPoint;
    public float cameraDistance;
    public DialogueObject defaultDialogue;
    public List<DialogueCondition> conditionalDialogues;

    private void Start()
    {
        prompt = "Talk";
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(this);
    }

    public DialogueObject GetDialogue()
    {
        foreach (DialogueCondition condition in conditionalDialogues)
        {
            if (condition.condition.Evaluate() == condition.evaluateTo)
            {
                return condition.dialogue;
            }
        }

        return defaultDialogue;
    }
}

[System.Serializable]
public class DialogueCondition
{
    public Condition condition;
    public bool evaluateTo = true;
    public DialogueObject dialogue;
}
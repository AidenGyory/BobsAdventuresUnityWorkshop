using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueContainer : Interactable
{
    public Transform cameraPoint;
    public float cameraDistance;

    [Header("Default Dialogue")]
    public DialogueObject defaultDialogue;
    public UnityEvent onDefaultDialogueEnd;

    public List<DialogueCondition> conditionalDialogues;

    private void Start()
    {
        prompt = "Talk";
    }

    public override void Interact()
    {
        DialogueManager.instance.StartDialogue(this);
    }

    public DialogueObject GetDialogue(out UnityEvent onEnd)
    {
        foreach (DialogueCondition condition in conditionalDialogues)
        {
            if (condition.condition.Evaluate() == condition.evaluateTo)
            {
                onEnd = condition.onDialogueEnd;
                return condition.dialogue;
            }
        }

        onEnd = onDefaultDialogueEnd;
        return defaultDialogue;
    }
}

[System.Serializable]
public class DialogueCondition
{
    public Condition condition;
    public bool evaluateTo = true;
    public DialogueObject dialogue;
    public UnityEvent onDialogueEnd;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    public List<DialogueLine> lines;
}

[System.Serializable]
public class DialogueLine
{
    public string name;
    public string line;
}

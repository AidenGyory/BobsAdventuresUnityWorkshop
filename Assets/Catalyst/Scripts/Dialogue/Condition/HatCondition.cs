using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatCondition : Condition
{
    public Transform hatParent;

    public override bool Evaluate()
    {
        foreach (Transform child in hatParent.transform)
        {
            if (child.gameObject.name.ToLower() == "hat")
            {
                return true;
            }
        }
        return false;
    }
}

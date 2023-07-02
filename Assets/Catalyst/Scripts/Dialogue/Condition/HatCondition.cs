using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatCondition : Condition
{
    public Transform hatParent;

    public override bool Evaluate()
    {
        if (hatParent.childCount > 0)
        {
            return hatParent.GetChild(0).gameObject.name.ToLower() == "hat";
        }
        return false;
    }
}

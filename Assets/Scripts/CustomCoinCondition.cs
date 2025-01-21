using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCoinCondition : Condition
{
    [SerializeField]
    private int neededCoins = 0; 

    public override bool Evaluate()
    {
        return Coin.coinCount >= neededCoins;
    }
}

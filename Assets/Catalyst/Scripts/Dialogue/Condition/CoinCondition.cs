using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCondition : Condition
{
    int neededCoins = 1000000;

    public override bool Evaluate()
    {
        return Coin.coinCount >= neededCoins;
    }
}

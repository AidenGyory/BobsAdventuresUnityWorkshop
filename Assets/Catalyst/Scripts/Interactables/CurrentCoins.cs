using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCoins : MonoBehaviour
{
    public int currentCoins;

    void Update() {
        Coin.coinCount = currentCoins;
        InteractManager.instance.SetCoinText();
    }
    
    public void AddCoins(int amount)
    {
        currentCoins += amount; 
    }
}

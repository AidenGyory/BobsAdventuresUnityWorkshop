using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCoins : MonoBehaviour
{
    [SerializeField]
    private int initialCoins = 0; // Editable in the Inspector

    private void Start()
    {
        // Initialize Coin.coinCount with the value from the Inspector.
        Coin.coinCount = initialCoins;
        InteractManager.instance.SetCoinText();
    }

    public void AddCoins(int amount)
    {
        // Add coins to the global coin count.
        Coin.coinCount += amount;
        
        // Update the UI or related systems.
        InteractManager.instance.SetCoinText();
    }

    public void SubtractCoins(int amount)
    {
        // Subtract coins, but ensure the result is not below zero.
        Coin.coinCount = Mathf.Max(0, Coin.coinCount - amount);

        // Update the UI or related systems.
        InteractManager.instance.SetCoinText();
    }

    public void SetCoins(int amount)
    {
        // Set a specific coin amount, ensuring it is not negative.
        Coin.coinCount = Mathf.Max(0, amount);

        // Update the UI or related systems.
        InteractManager.instance.SetCoinText();
    }
}

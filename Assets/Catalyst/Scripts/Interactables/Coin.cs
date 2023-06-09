using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interactable
{
    public static int coinCount = 0;

    private void Start()
    {
        prompt = "Pickup coin";
    }
    public override void Interact()
    {
        coinCount++;
        InteractManager.instance.HidePrompt();
        InteractManager.instance.SetCoinText();
        Destroy(gameObject);
    }
}

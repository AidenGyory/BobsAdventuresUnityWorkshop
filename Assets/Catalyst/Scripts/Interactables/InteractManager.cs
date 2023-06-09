using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractManager : MonoBehaviour
{
    public TextMeshProUGUI promptText, coinText;
    public GameObject prompt;

    public static InteractManager instance;
    private void Awake()
    {
        instance = this;
        prompt.SetActive(false);
    }

    public void ShowPrompt(string prompt)
    {
        promptText.text = prompt;
        this.prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        prompt.SetActive(false);
    }

    public void SetCoinText()
    {
        coinText.text = "Coins: " + Coin.coinCount;
    }
}

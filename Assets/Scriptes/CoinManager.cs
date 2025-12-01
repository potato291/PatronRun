using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;

    private void Start()
    {
        coinText.text = "Score: " + coinCount.ToString();
    }

    public void AddCoin()
    {
        coinCount++;

        coinText.text = "Score: " + coinCount.ToString();
    }
}

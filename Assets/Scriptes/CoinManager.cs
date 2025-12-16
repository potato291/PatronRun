using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coinCount;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateText();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateText();
    }

    void UpdateText()
    {
        if (coinText != null)
            coinText.text = "Score: " + coinCount.ToString();
    }
}
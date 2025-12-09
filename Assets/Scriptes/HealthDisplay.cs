using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

            
            if (i < playerHealth.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart; 
            }
        }
    }
}
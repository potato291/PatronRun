using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float iFramesDuration = 1.5f;
    public int currentHealth;

    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("ОШИБКА: На игроке нет картинки (SpriteRenderer)! Скрипт не сможет мигать.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadly"))
        {
            Die();
        }
    }

    public void Die()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        if (spriteRenderer != null)
        {
            for (int i = 0; i < 8; i++)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / 16);
                spriteRenderer.color = new Color(1, 1, 1, 1f);
                yield return new WaitForSeconds(iFramesDuration / 16);
            }
        }
        else
        {
            yield return new WaitForSeconds(iFramesDuration);
        }

        isInvulnerable = false;
    }
}
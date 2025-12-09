using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteraction : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.contacts[0];

            if (contact.normal.y < -0.5f)
            {
                KillEnemy(collision.gameObject);
            }
            else
            {
                HitPlayer(collision.gameObject);
            }
        }
    }

    void KillEnemy(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
        }

        Destroy(gameObject);
    }

    void HitPlayer(GameObject player)
    {
        PlayerHealth healthScript = player.GetComponent<PlayerHealth>();

        if (healthScript != null)
        {
            healthScript.TakeDamage(1);
        }
    }
}
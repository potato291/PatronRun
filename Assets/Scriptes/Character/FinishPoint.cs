using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

       
        int nextSceneIndex = currentSceneIndex + 1;

      
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {       
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Игра пройдена! Возвращаемся на старт.");
            SceneManager.LoadScene(0);
        }
    }
}
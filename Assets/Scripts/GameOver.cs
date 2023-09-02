using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    [SerializeField] private GameObject gameOverMenu;
    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }
    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
   
}

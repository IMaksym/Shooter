using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public GameObject winScreenUI;
    private bool winScreenShown = false;

    private void Start()
    {
        winScreenUI.SetActive(false);
    }

    private void Update()
    {
        if (!winScreenShown && AllEnemiesDefeated())
        {
            ShowWinScreen();
        }
    }

    public void ShowWinScreen()
    {
        winScreenUI.SetActive(true);
        winScreenShown = true;
    }

    private bool AllEnemiesDefeated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                return false; // Не все враги уничтожены
            }
        }
        return true; // Все враги уничтожены
    }
    
    public void Again ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 0);
    }

    public void Exit ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}

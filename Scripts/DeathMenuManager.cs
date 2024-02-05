using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public GameObject deathMenuUI;
    public Button restartButton;
    public Button quitButton;

    private bool isPlayerDead = false;

    private void Start()
    {
        deathMenuUI.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (isPlayerDead && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void ShowDeathMenu()
    {
        deathMenuUI.SetActive(true);
    }

    public void SetPlayerDead(bool value)
    {
        isPlayerDead = value;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}

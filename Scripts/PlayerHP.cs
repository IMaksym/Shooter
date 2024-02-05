using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerHP : MonoBehaviour
{
    public float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image HpDown;
    public Image Hp;
    public DeathMenuManager deathMenuManager;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(Random.Range(5, 10));
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = Hp.fillAmount;
        float fillB = HpDown.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            Hp.fillAmount = hFraction;
            HpDown.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            HpDown.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    private void Die()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available!");
        }

        deathMenuManager.ShowDeathMenu();
        deathMenuManager.SetPlayerDead(true);
        Destroy(gameObject);
    }
}

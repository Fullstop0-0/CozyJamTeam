using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    public TextMeshProUGUI healthText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;

        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameLose");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        if(health <= 0)
        {
        
            gameObject.SetActive(false);
            GameOver();
            Debug.Log("Dead");
        }
    }
}

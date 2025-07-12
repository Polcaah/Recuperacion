using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private int startingLives = 3;

    private float currentHealth;
    private int currentLives;
    private bool dead;

    private Vector3 spawnPosition;

    private void Awake()
    {
        currentHealth = startingHealth;
        currentLives = startingLives;
        spawnPosition = transform.position; 
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log(currentHealth);

        if (currentHealth > 0)
        {
        }
        else
        {
            if (!dead)
            {
                dead = true;
                GetComponent<PlayerMovement>().enabled = false;

                currentLives--;

                Invoke(nameof(Respawn), 0.3f);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Respawn()
    {
        if (currentLives > 0)
        {
            transform.position = spawnPosition;
            currentHealth = startingHealth;
            dead = false;
            GetComponent<PlayerMovement>().enabled = true;

            if (GameManager.instance != null)
                GameManager.instance.AddScore(-20);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public int GetLives()
    {
        return currentLives;
    }
}

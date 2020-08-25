using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float startHealth = 100f;
    private float health;
    public int worth = 50;

    [HideInInspector]
    public float speed;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    private SinglePlayAudio singlePlayAudio;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        singlePlayAudio = SinglePlayAudio.instance;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        PlayerStats.Money += worth;

        GameObject death = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(death, 5f);

        WaveSpawner.enemiesAlive--;

        singlePlayAudio.PlayEnemyDeathClip();

        Destroy(gameObject);
    }

    public void Slow(float percentage)
    {
        speed = startSpeed * (1 - percentage);
    }
}

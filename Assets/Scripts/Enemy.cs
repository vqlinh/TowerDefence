using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private bool isDeadrun = false;
    public bool IsDeadrun
    {
        get { return isDeadrun; }
    }

    public float startHealth = 100;
    public float startSpeed = 10f;

    [Header("enemy $$$")]
    public int worth = 50;

    public GameObject deathEffect;
    public float health;
    public float speed = 10f;
    [Header("enemy health")]
    public Image healthBar;

    private bool isDead = false;
    private void Start()
    {
        health = startHealth;
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead) Die();
    }

    void Die()
    {
        isDeadrun = true;
        GameManager.instance.AudioEnemyDie();
        isDead = true;
        GameObject effect = GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.Money += worth;
        WaveSpawner.enemiesALives--;
        Destroy(gameObject);
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
}

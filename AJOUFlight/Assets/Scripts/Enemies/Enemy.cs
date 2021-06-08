using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int hp;
    private double moneyAmount;
    private int scoreAmount;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private GameObject enemyBullet;

    private Shoot shoot;


    public int Hp
    {
        get { return hp; }
        set { if (value >= 0) hp = value; else hp = 0; }
    }

    public double MoneyAmount
    {
        get { return moneyAmount; }
        set { if (value >= 0) moneyAmount = value; else moneyAmount = 0; }
    }

    public int ScoreAmount
    {
        get { return scoreAmount; }
        set { if (value >= 0) scoreAmount = value; else scoreAmount = 0; }
    }


    void Start()
    {
        if (GameManager.Instance.bossTime) {
            healthBar = GameObject.Find("Boss Canvas").transform.GetChild(0).GetComponent<HealthBar>();
            healthBar.SetMaxHealth((GameManager.Instance.type+1) * 200);
            healthBar.SetHealth((GameManager.Instance.type+1) * 200);
        }
        else{
            healthBar.SetMaxHealth(Hp);
        }

        PerformShoot();
    }

    void Update()
    {
        
    }


    public void SetShoots(Shoot s)
    {
        if (s == null) {
            Debug.Log("shoot is null");
            return ;
        }

        shoot = s;
        shoot.EnemyBullet = enemyBullet;
    }


    public void PerformShoot()
    {
        if(shoot != null)
            shoot.Shoots();
    }


    private void TakeDamage(int damage)
    {
        Hp -= damage;
        healthBar.SetHealth(Hp);

        if (Hp <= 0) 
            OnDead();
    }


    protected virtual void OnDead()
    {
        GameManager.Instance.RemoveEnemy(gameObject);
        GameManager.Instance.AddScore(ScoreAmount);
        GameManager.Instance.AddGameMoney(MoneyAmount);
        AudioManager.Instance.PlayEnemyDeathClip();
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
            AudioManager.Instance.PlayEnemyHitClip();
            bullet.gameObject.SetActive(false);
        }
    }


    private void ExcapeSoul()
    {
        // Escape the soul ...
    }
}

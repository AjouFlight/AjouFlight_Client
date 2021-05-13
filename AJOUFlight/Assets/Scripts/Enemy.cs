using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private double moneyAmount;
    [SerializeField]
    private int scoreAmount;
    [SerializeField]
    private HealthBar healthBar;

    public GameObject enemyBullet;

    void Start()
    {
        healthBar.SetMaxHealth(hp);
    }

    void Update()
    {
        
    }


    protected virtual void pattern()
    {
        // moving and attack pattern ... 
    }


    private void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) OnDead();
        Debug.Log(hp);
        healthBar.SetHealth(hp);
    }


    protected virtual void OnDead()
    {
        GameManager.Instance.AddScore(scoreAmount);
        GameManager.Instance.Money += 10;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
            AudioSource bulletAudio = bullet.GetComponent<AudioSource>();
            bulletAudio.Play();
        }
    }

}

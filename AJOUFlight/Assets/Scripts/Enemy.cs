using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int Hp { 
        get { return Hp; } 
        set { if (Hp < 0) Hp = 0; else Hp = value; }
    }

    private int moneyAmount;
    private int scoreAmount;

    [SerializeField]
    private Sprite sprite;
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    protected virtual void pattern()
    {
        // moving and attack pattern ... 
    }


    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0) OnDead();
    }


    protected virtual void OnDead()
    {
        // When the enemy die ...
    }

}

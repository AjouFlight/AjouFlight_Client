using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shoot : MonoBehaviour
{
    private GameObject flightBullet;
    private GameObject enemyBullet;
    private int attackDamage;
    private float speed;

    public GameObject FlightBullet
    {
        get { return flightBullet; }
        set { if (value != null) flightBullet = value; }
    }

    public GameObject EnemyBullet
    {
        get { return enemyBullet; }
        set { if (value != null) enemyBullet = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { if (value >= 0) speed = value; else speed = 0; }
    }

    public int AttackDamage
    {
        get { return attackDamage; }
        set { if (value >= 0) attackDamage = value; else attackDamage = 0; }
    }

    public virtual void Shoots() { }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private float speed;
    private int damage;

    public float Speed
    {
        get { return speed; }
        set { if (value < 0) speed = 0; else speed = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { if (value < 0) damage = 0; else damage = value; }
    }

    private Rigidbody2D bulletRigid;


    void Awake()
    {
        Destroy(this, 7.0f);
        bulletRigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        MoveForward();
    }


    public void MoveForward()
    {
        bulletRigid.velocity = transform.TransformDirection(Vector2.up) * Speed;
    }


    public void SetBullet(float speed, int damage)
    {
        Speed = speed;
        Damage = damage;
    }
}

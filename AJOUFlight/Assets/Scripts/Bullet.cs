using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public float Speed
    {
        get { return Speed; }
        private set { if (Speed < 0) Speed = 0; else Speed = value; }
    }

    public int Damage
    {
        get { return Damage; }
        private set { if (Damage < 0) Damage = 0; else Damage = value; }
    }

    private Rigidbody2D bulletRigid;


    void Awake()
    {
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

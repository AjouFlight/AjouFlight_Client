using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private float speed;
    private int damage;
    private Rigidbody2D bulletRigid;

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


    void Awake()
    {
        Destroy(gameObject, 7.0f);
        bulletRigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        MoveForward();
    }


    /********************************************
    * Function : MoveForward()
    * descrition : Bullets move upwards in Flight.
    ********************************************/
    public void MoveForward()
    {
        bulletRigid.velocity = transform.TransformDirection(Vector2.up) * Speed;
    }


    /********************************************
    * Function : SetBullet(float speed, int damage)
    * descrition : Set the bullet’s stat.
    ********************************************/
    public void SetBullet(float speed, int damage)
    {
        Speed = speed;
        Damage = damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Rigidbody2D bulletRigid;

    private enum Type { Player = 0, Enemy }
    private Type type;
    private float speed;
    private int damage;
    
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
         bulletRigid.velocity = transform.TransformDirection(Vector2.up) * speed;
    }


    public void SetBullet(int typeIndex, float speed, int damage)
    {
        switch (typeIndex)
        {
            case 0:
                type = Type.Player;
                break;
            case 1:
                type = Type.Enemy;
                break;
            default:
                break;
        }

        this.speed = speed;
        this.damage = damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && type == Type.Player)
        {
            // audioSource.Play();
            // takeDamage
            // Enemy hit.
        }
        else if(collision.CompareTag("Player") && type == Type.Enemy)
        {
            // audioSource.Play();
            // takeDamage
            // Player hit.
        }
    }
}

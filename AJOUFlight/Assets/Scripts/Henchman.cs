using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henchman : Enemy
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int bulletDamage;


    void Start()
    {
        StartCoroutine(Shoot());
    }

    
    void Update()
    {
        
    }


    protected override void OnDead()
    {
        base.OnDead();
        Destroy(gameObject);
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject bullet = Instantiate(enemyBullet, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetBullet(bulletSpeed, bulletDamage);
            yield return new WaitForSeconds(0.5f);
        }
    }


    private void ExcapeSoul()
    {
        // Escape the soul ...
    }
}

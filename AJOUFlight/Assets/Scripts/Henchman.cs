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


    IEnumerator Shoot()
    {
        while (true)
        {
            bullet.GetComponent<Bullet>().SetBullet(bulletSpeed, bulletDamage);
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }


    private void ExcapeSoul()
    {
        // Escape the soul ...
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerCShoot : Shoot
{
    private readonly float startSpeed = 7f;
    private readonly int startAttackDamage = 30;
    private readonly float shootTime = 0.8f;

    public override void Shoots()
    {
        Speed = startSpeed;
        AttackDamage = startAttackDamage;
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        float x = 0;
        Quaternion q = transform.rotation;

        while (true)
        {
            for(int i=0; i<2; i++)
            {
                x = 0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet = Instantiate(EnemyBullet, transform.position, q);
                bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                x = 45.0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet2 = Instantiate(EnemyBullet, transform.position, q);
                bullet2.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                x = 180f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet3 = Instantiate(EnemyBullet, transform.position, q);
                bullet3.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                x = 225.0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet4 = Instantiate(EnemyBullet, transform.position, q);
                bullet4.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                x = 135.0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet5 = Instantiate(EnemyBullet, transform.position, q);
                bullet5.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                x = 315.0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet6 = Instantiate(EnemyBullet, transform.position, q);
                bullet6.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                yield return new WaitForSeconds(shootTime);
            }
            
            yield return new WaitForSeconds(2.0f);
        }
        
    }
}

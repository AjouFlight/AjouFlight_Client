using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerAShoot : Shoot
{
    private readonly float startSpeed = 5f;
    private readonly int startAttackDamage = 10;
    private readonly float shootTime = 1.0f;

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
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                GameObject bullet = Instantiate(EnemyBullet, transform.position, q);
                bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);
            }
            else if(random == 1)
            {    
                x = 210.0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet = Instantiate(EnemyBullet, transform.position, q);
                bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

                x = 150.0f;
                q = Quaternion.Euler(0, 0, x);
                GameObject bullet2 = Instantiate(EnemyBullet, transform.position, q);
                bullet2.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);
            }

            yield return new WaitForSeconds(shootTime);
        }
    }
}

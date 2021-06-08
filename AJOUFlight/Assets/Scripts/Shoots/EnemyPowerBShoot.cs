using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerBShoot : Shoot
{
    private readonly float startSpeed = 6f;
    private readonly int startAttackDamage = 20;
    private readonly float shootTime = 3.0f;

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
            x = 0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet = Instantiate(EnemyBullet, transform.position, q);
            bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 45.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet2 = Instantiate(EnemyBullet, transform.position, q);
            bullet2.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 90.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet3 = Instantiate(EnemyBullet, transform.position, q);
            bullet3.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 135.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet4 = Instantiate(EnemyBullet, transform.position, q);
            bullet4.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 180.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet5 = Instantiate(EnemyBullet, transform.position, q);
            bullet5.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 225.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet6 = Instantiate(EnemyBullet, transform.position, q);
            bullet6.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 270.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet7 = Instantiate(EnemyBullet, transform.position, q);
            bullet7.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            x = 315.0f;
            q = Quaternion.Euler(0, 0, x);
            GameObject bullet8 = Instantiate(EnemyBullet, transform.position, q);
            bullet8.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            yield return new WaitForSeconds(shootTime);
        }
    }
}

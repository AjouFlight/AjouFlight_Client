using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBShoot : Shoot
{
    private readonly float startSpeed = 7f;
    private readonly int startAttackDamage = 100;

    public override void Shoots()
    {
        Speed = startSpeed;
        AttackDamage = startAttackDamage;
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        float x = 0.0f;
        Quaternion q = transform.rotation;
        while (true)
        {
            GameObject bullet = Instantiate(EnemyBullet, transform.position, q);
            bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);
            bullet.transform.localScale *= 1.5f;

            x += 10.0f;
            q = Quaternion.Euler(0, 0, x);

            yield return new WaitForSeconds(0.2f);
        }
    }
}


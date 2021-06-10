using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAShoot : Shoot
{
    private readonly float startSpeed = 6f;
    private readonly int startAttackDamage = 50;

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

            x += 100;
            q = Quaternion.Euler(0, 0, x);

            yield return new WaitForSeconds(0.05f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCShoot : Shoot
{
    private readonly float startSpeed = 8f;
    private readonly int startAttackDamage = 150;

    public override void Shoots()
    {
        Speed = startSpeed;
        AttackDamage = startAttackDamage;
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        bool flag = true;
        float x = 135.0f;
        Quaternion q = transform.rotation;
        while (true)
        {
            GameObject bullet = Instantiate(EnemyBullet, transform.position, q);
            bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);
            bullet.transform.localScale *= 1.5f;

            if (x > 225.0f) flag = false;
            else if (x < 135.0f) flag = true;

            if (flag) x += 10.0f;
            else x -= 10.0f;

            q = Quaternion.Euler(0, 0, x);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
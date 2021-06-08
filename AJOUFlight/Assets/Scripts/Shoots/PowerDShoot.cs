using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDShoot : Shoot
{
    private readonly float startSpeed = 6.6f;
    private readonly int startAttackDamage = 40;
    private readonly float shootTime = 0.14f;

    public override void Shoots()
    {
        Speed = startSpeed;
        AttackDamage = startAttackDamage;
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            GameObject bullet = Instantiate(FlightBullet, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetBullet(Speed, AttackDamage);

            yield return new WaitForSeconds(shootTime);
        }
    }
}

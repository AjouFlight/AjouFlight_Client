using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAShoot : Shoot
{
    private readonly float startSpeed = 6f;
    private readonly int startAttackDamage = 10;
    private readonly float shootTime = 0.2f;

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

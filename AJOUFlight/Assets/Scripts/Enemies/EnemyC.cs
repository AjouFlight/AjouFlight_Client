using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy
{
    private readonly int startHp = 70;
    private readonly int score = 30;
    private readonly double moeny = 30;

    void Awake()
    {
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = moeny;
        EnemyPowerCShoot enemyPowerCShoot = gameObject.AddComponent<EnemyPowerCShoot>();
        SetShoots(enemyPowerCShoot);
    }

    void Update()
    {
        
    }
}

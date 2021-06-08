using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{
    private readonly int startHp = 50;
    private readonly int score = 20;
    private readonly double moeny = 20;

    void Awake()
    {
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = moeny;
        EnemyPowerBShoot enemyPowerBShoot = gameObject.AddComponent<EnemyPowerBShoot>();
        SetShoots(enemyPowerBShoot);
    }

    void Update()
    {
        
    }
}

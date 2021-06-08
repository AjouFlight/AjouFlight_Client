using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    private readonly int startHp = 30;
    private readonly int score = 10;
    private readonly double moeny = 10;

    void Awake()
    {
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = moeny;
        EnemyPowerAShoot enemyPowerAShoot = gameObject.AddComponent<EnemyPowerAShoot>();
        SetShoots(enemyPowerAShoot);
    }

    void Update()
    {
        
    }
}

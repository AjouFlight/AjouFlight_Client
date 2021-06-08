using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossA : Enemy
{
    private readonly int startHp = 200;
    private readonly int score = 50;
    private readonly double money = 50;

    void Awake()
    {
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = money;
        BossAShoot bossAShoot = gameObject.AddComponent<BossAShoot>();
        SetShoots(bossAShoot);
    }

    protected override void OnDead()
    {
        GameManager.Instance.AddScore(ScoreAmount);
        GameManager.Instance.AddGameMoney(MoneyAmount);
        GameManager.Instance.BossDead();
        Destroy(gameObject);
    }
}

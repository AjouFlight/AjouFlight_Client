public class EnemyB : Enemy
{
    private readonly int startHp = 50;
    private readonly int score = 20;
    private readonly double moeny = 20;

    void Awake()
    {
        // Set the enemy stat
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = moeny;

        // Set the shoot type
        EnemyPowerBShoot enemyPowerBShoot = gameObject.AddComponent<EnemyPowerBShoot>();
        SetShoots(enemyPowerBShoot);
    }

}

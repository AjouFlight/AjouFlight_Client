public class EnemyA : Enemy
{
    private readonly int startHp = 30;
    private readonly int score = 10;
    private readonly double moeny = 10;

    void Awake()
    {
        // Set the enemy stat
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = moeny;

        // Set the shoot type
        EnemyPowerAShoot enemyPowerAShoot = gameObject.AddComponent<EnemyPowerAShoot>();
        SetShoots(enemyPowerAShoot);
    }

}

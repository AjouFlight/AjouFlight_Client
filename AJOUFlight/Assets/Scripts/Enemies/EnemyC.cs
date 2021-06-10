public class EnemyC : Enemy
{
    private readonly int startHp = 70;
    private readonly int score = 30;
    private readonly double moeny = 30;

    void Awake()
    {
        // Set the enemy stat
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = moeny;

        // Set the shoot type
        EnemyPowerCShoot enemyPowerCShoot = gameObject.AddComponent<EnemyPowerCShoot>();
        SetShoots(enemyPowerCShoot);
    }

}

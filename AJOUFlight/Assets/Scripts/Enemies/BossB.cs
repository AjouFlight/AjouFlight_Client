public class BossB : Enemy
{
    private readonly int startHp = 400;
    private readonly int score = 100;
    private readonly double money = 100;

    void Awake()
    {
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = money;
        BossBShoot bossBShoot = gameObject.AddComponent<BossBShoot>();
        SetShoots(bossBShoot);
    }

    /********************************************
    * Function : OnDead()
    * descrition : 
    *  - When the boss is died, add score and money.
    *  - Call BossDead method for showing result panel.
    ********************************************/
    protected override void OnDead()
    {
        GameManager.Instance.AddScore(ScoreAmount);
        GameManager.Instance.AddGameMoney(MoneyAmount);
        GameManager.Instance.BossDead();
        Destroy(gameObject);
    }
}
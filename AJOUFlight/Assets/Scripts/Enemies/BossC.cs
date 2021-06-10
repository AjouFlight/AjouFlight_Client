public class BossC : Enemy
{
    private readonly int startHp = 600;
    private readonly int score = 150;
    private readonly double money = 150;

    void Awake()
    {
        Hp = startHp;
        ScoreAmount = score;
        MoneyAmount = money;
        BossCShoot bossCShoot = gameObject.AddComponent<BossCShoot>();
        SetShoots(bossCShoot);
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
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int hp;
    private double moneyAmount;
    private int scoreAmount;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private GameObject enemyBullet;

    private Shoot shoot;


    public int Hp
    {
        get { return hp; }
        set { if (value >= 0) hp = value; else hp = 0; }
    }

    public double MoneyAmount
    {
        get { return moneyAmount; }
        set { if (value >= 0) moneyAmount = value; else moneyAmount = 0; }
    }

    public int ScoreAmount
    {
        get { return scoreAmount; }
        set { if (value >= 0) scoreAmount = value; else scoreAmount = 0; }
    }


    void Start()
    {
        if (GameManager.Instance.IsBossTime) {
            healthBar = GameObject.Find("Boss Canvas").transform.GetChild(0).GetComponent<HealthBar>();
            healthBar.SetMaxHealth((GameManager.Instance.Type+1) * 200);
            healthBar.SetHealth((GameManager.Instance.Type+1) * 200);
        }
        else{
            healthBar.SetMaxHealth(Hp);
        }

        PerformShoot();
    }

    /********************************************
    * Function : SetShoots(Shoot s)
    * descrition : select the shoot type.
    ********************************************/
    public void SetShoots(Shoot s)
    {
        if (s == null) {
            Debug.Log("shoot is null");
            return ;
        }

        shoot = s;
        shoot.EnemyBullet = enemyBullet;
    }


    /********************************************
    * Function : PerformShoot()
    * descrition : Execute the shoot.
    ********************************************/
    public void PerformShoot()
    {
        if(shoot != null)
            shoot.Shoots();
    }


    /********************************************
    * Function : TakeDamage(int damage)
    * descrition : When the enemy collides with the player bullet, 
    *           the enemy Hp is reduced. When Hp <= 0, the enemy will die.
    ********************************************/
    private void TakeDamage(int damage)
    {
        Hp -= damage;
        healthBar.SetHealth(Hp);

        if (Hp <= 0) 
            OnDead();
    }


    /********************************************
    * Function : OnDead()
    * descrition : 
    *  - When the enemy Hp is 0, call this method. 
    *  - OnDead method is different from normal enemy and boss.
    ********************************************/
    protected virtual void OnDead()
    {
        GameManager.Instance.RemoveEnemy(gameObject);
        GameManager.Instance.AddScore(ScoreAmount);
        GameManager.Instance.AddGameMoney(MoneyAmount);
        AudioManager.Instance.PlayEnemyDeathClip();
        Destroy(gameObject);
    }


    /********************************************
    * Function : OnTriggerEnter2D(Collider2D collision)
    * descrition : 
    *  - When the enemy collides with something, this method will be called. 
    *  - If something is player bullet, TakeDamage method will be called.
    ********************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
            AudioManager.Instance.PlayEnemyHitClip();
            bullet.gameObject.SetActive(false);
        }
    }

}

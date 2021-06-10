using UnityEngine;

public abstract class Player : MonoBehaviour
{
    private int id;
    private int hp;
    private float speed;

    private MovementJoystick movementJoystick;
    private ShootingJoystick shootingJoystick;

    [SerializeField]
    private Rigidbody2D playerRigid;

    [SerializeField]
    private HealthBar healthBar;
    
    [SerializeField]
    private GameObject bullet;

    // Strategy
    private Shoot shoot;


    public int Id
    {
        get { return id; }
        set { if (value >= 0) id = value; else Debug.Log("Invalid Id."); }
    }
    public int Hp
    {
        get { return hp; }
        set { if (value < 0) hp = 0; else hp = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { if (value < 0) speed = 0; else speed = value; }
    }
    public MovementJoystick MoveJoystick
    {
        get { return movementJoystick; }
        set { if (value != null) movementJoystick = value; }
    }

    public ShootingJoystick Shootjoystick
    {
        get { return shootingJoystick; }
        set { if (value != null) shootingJoystick = value; }
    }


    void Start()
    {
        int flightIndex = PlayerInformation.selectedFlight;
        Hp = (flightIndex + 1) * 100;
        Speed = 3.0f + (flightIndex + 1) * 0.2f;

        healthBar.SetMaxHealth(Hp);
        PerformShoot();
    }


    protected virtual void Update()
    {
        Move();
        Rotate();
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
        shoot.FlightBullet = bullet;
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
    * Function : Move()
    * descrition : The player moves according to joystick direction.
    ********************************************/
    private void Move()
    {
        float x = MoveJoystick.moveJoystickVec2.x;
        float y = MoveJoystick.moveJoystickVec2.y;

        Vector2 velocity = new Vector2(x, y);
        velocity = velocity * Speed;

        playerRigid.velocity = velocity;
    }


    /********************************************
    * Function : Rotate()
    * descrition : The player rotates according to joystick direction.
    ********************************************/
    private void Rotate()
    {
        Vector2 originVec = gameObject.transform.up;

        float angle = Vector2.Angle(Shootjoystick.shotJoystickVec2, originVec);
        int sign = (Vector3.Cross(Shootjoystick.shotJoystickVec2, originVec).z > 0f) ? -1 : 1;

        angle *= sign;

        gameObject.transform.Rotate(0, 0, angle);
    }


    /********************************************
    * Function : Rotate()
    * descrition : 
    *  - When the player collides with the Enemy Bullet, the Player Hp is reduced. 
    *  - When Hp <= 0, GameOver. And update Healthbar.
    ********************************************/
    private void TakeDamage(int damage)
    {
        Hp -= damage;
        if(Hp <= 0)
        {
            GameManager.Instance.GameOver();
            Destroy(this);
        }
        healthBar.SetHealth(Hp);
    }


    /********************************************
    * Function : OnTriggerEnter2D(Collider2D collision)
    * descrition : 
    *  - When the player collides with something, this method will be called. 
    *  - If something is enemy bullet, TakeDamage method will be worked.
    ********************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
            AudioManager.Instance.PlayPlayerHitClip();
            bullet.gameObject.SetActive(false);
        }
    }
}

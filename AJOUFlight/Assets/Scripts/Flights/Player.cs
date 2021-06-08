using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    private int id;
    private int hp;
    private float speed;

    [SerializeField]
    private Rigidbody2D playerRigid;

    private MovementJoystick movementJoystick;
    private ShootingJoystick shootingJoystick;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject[] colleages;

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
        Hp = (flightIndex + 1) * 100 + 1000;
        Speed = 3.0f + (flightIndex + 1) * 0.2f;

        healthBar.SetMaxHealth(Hp);
        PerformShoot();
    }


    protected virtual void Update()
    {
        Move();
        Rotate();
    }


    public void SetShoots(Shoot s)
    {
        if (s == null) {
            Debug.Log("shoot is null");
            return ;
        }
        shoot = s;
        shoot.FlightBullet = bullet;
    }


    public void PerformShoot()
    {
        if(shoot != null)
            shoot.Shoots();
    }


    private void Move()
    {
        float x = MoveJoystick.moveJoystickVec2.x;
        float y = MoveJoystick.moveJoystickVec2.y;

        Vector2 velocity = new Vector2(x, y);
        velocity = velocity * Speed;

        playerRigid.velocity = velocity;
    }


    private void Rotate()
    {
        Vector2 originVec = gameObject.transform.up;

        float angle = Vector2.Angle(Shootjoystick.shotJoystickVec2, originVec);
        int sign = (Vector3.Cross(Shootjoystick.shotJoystickVec2, originVec).z > 0f) ? -1 : 1;

        angle *= sign;

        gameObject.transform.Rotate(0, 0, angle);
    }


    private void AbsorbSoul()
    {
        // absorb the soul.
    }


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

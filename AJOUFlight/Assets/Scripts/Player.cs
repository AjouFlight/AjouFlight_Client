using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Id
    {
        get { return Id; }
        set { if (Id >= 0) Id = value; else Debug.Log("Invalid Id."); }
    }
    public int Hp
    {
        get { return Hp; }
        private set { if (Hp < 0) Hp = 0; else Hp = value; }
    }
    public float Speed
    {
        get { return Speed; }
        private set { if (Speed < 0) Speed = 0; else Speed = value; }
    }

    private Rigidbody2D playerRigid;
    public MovementJoystick movementJoystick;
    public ShootingJoystick shootingJoystick;

    public GameObject bullet; // For Prototype (temp variables)
    public GameObject bulletPivot; // For Prototype (temp variables)

    [SerializeField]
    private GameObject[] colleages;


    void Awake()
    {
        Speed = 3.0f;
        playerRigid = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        Move();
        Rotate();
    }


    private void Move()
    {
        float x = movementJoystick.moveJoystickVec2.x;
        float y = movementJoystick.moveJoystickVec2.y;

        Vector2 velocity = new Vector2(x, y);
        velocity = velocity * Speed;
        playerRigid.velocity = velocity;
    }


    private void Rotate()
    {
        Vector2 originVec = gameObject.transform.up;

        float angle = Vector2.Angle(shootingJoystick.shotJoystickVec2, originVec);
        int sign = (Vector3.Cross(shootingJoystick.shotJoystickVec2, originVec).z > 0f) ? -1 : 1;

        angle *= sign;

        gameObject.transform.Rotate(0, 0, angle);
    }


    protected virtual IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bullet, bulletPivot.transform.position, bulletPivot.transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
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
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
            AudioSource bulletAudio = bullet.GetComponent<AudioSource>();
            bulletAudio.Play();
        }
    }
}

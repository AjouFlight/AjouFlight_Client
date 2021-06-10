public class FlightC : Player
{
    void Awake()
    {
        // Set the shoot type
        PowerAShoot powerAShoot = gameObject.AddComponent<PowerAShoot>();
        SetShoots(powerAShoot);
    }

    protected override void Update()
    {
        base.Update();
    }
}

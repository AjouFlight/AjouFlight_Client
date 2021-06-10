public class FlightB : Player
{
    void Awake()
    {
        // Set the shoot type
        PowerBShoot powerBShoot = gameObject.AddComponent<PowerBShoot>();
        SetShoots(powerBShoot);
    }

    protected override void Update()
    {
        base.Update();
    }
}

﻿public class FlightD : Player
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

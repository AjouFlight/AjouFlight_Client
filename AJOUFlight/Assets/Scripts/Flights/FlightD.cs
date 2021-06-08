using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightD : Player
{
    void Awake()
    {
        PowerAShoot powerAShoot = gameObject.AddComponent<PowerAShoot>();
        SetShoots(powerAShoot);
    }

    protected override void Update()
    {
        base.Update();
    }
}

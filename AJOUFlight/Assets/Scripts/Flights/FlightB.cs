using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightB : Player
{
    void Awake()
    {
        PowerBShoot powerBShoot = gameObject.AddComponent<PowerBShoot>();
        SetShoots(powerBShoot);
    }

    protected override void Update()
    {
        base.Update();
    }
}

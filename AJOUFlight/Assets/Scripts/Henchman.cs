using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henchman : Enemy
{
    private float speed;
    
    void Start()
    {
        StartCoroutine(Shoot());
    }

    
    void Update()
    {
        
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            // Shoot the enemy bullet.
            yield return new WaitForSeconds(0.5f);
        }
    }


    private void ExcapeSoul()
    {
        // Escape the soul ...
    }
}

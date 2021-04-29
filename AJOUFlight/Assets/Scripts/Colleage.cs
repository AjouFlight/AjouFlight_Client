using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleage : MonoBehaviour
{
    private int hp;
    private enum type { Henchman_1=0, Henchman_2, Henchman_3}


    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        // Shoot bullets.
        yield return null;
    }
}

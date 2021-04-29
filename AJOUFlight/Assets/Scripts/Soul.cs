using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private float lifeTime;
    private enum type { Henchman_1 = 0, Henchman_2, Henchman_3 }


    void Start()
    {
        lifeTime = 10.0f;
        StartCoroutine(Disappear(lifeTime));
    }


    IEnumerator Disappear(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this);
    }
}

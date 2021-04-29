using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : Player
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override IEnumerator Shoot()
    {
        while (true)
        {
            // Now, this is same with super.
            // But it will be changed later.
            Instantiate(bullet, bulletPivot.transform.position, bulletPivot.transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }
}

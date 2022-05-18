using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : AbstractBullet
{

/*    public override  void init(Vector3 startingHeading, float startingSpeed)
    {
        base.init(startingHeading,startingSpeed);
        heading = startingHeading;
        rb.velocity = startingHeading * (speed);
    }*/
    public override void init(float bulletSpeed,Vector3 direction,GameObject shooter)
    {
        base.init(bulletSpeed,direction,shooter);
        //rb.velocity = (speed + Vector3.Dot((direction).normalized,weaponVelocityAtShoot))* (direction).normalized; // not really needed
    }
}

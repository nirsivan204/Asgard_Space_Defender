using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : AbstractWeapon
{
    [SerializeField] float shootingSpeed;
    private AbstractShip ship;
   // private Rigidbody shipRB;

    public override void init(AbstractShip ship)
    {
        this.ship = ship;
        //shipRB = ship.GetComponent<Rigidbody>();
    }

    public override void shoot()
    {
        base.shoot();
        //((CannonBullet)lastBullet).init(shipRB.velocity, shootingSpeed,heading.position-transform.position);
        lastBullet.init(shootingSpeed,heading.position-transform.position, ship.gameObject,damage);
        //lastBullet.init(Heading.position, shootingSpeed + Vector3.Dot(shipRB.velocity,transform.up));
    }



    /*    public void FixedUpdate()
        {
        }*/
}

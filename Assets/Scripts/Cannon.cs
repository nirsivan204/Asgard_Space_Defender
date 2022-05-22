using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : AbstractWeapon
{
    [SerializeField] float bulletSpeed;
    [SerializeField] AbstractBullet bullet;
    protected AbstractBullet lastBullet;

    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

    public override void Shoot()
    {
        if (canShoot)
        {
            base.Shoot();
            //((CannonBullet)lastBullet).init(shipRB.velocity, shootingSpeed,heading.position-transform.position);
            lastBullet = Instantiate(bullet, transform.position, transform.rotation);
            lastBullet.init(bulletSpeed, heading.position - transform.position, ship, damage);
            gameMGR.musicMGR.Play_Sound(MusicMGR.SoundTypes.CannonShoot);
            //lastBullet.init(Heading.position, shootingSpeed + Vector3.Dot(shipRB.velocity,transform.up));
        }
    }



    /*    public void FixedUpdate()
        {
        }*/
}

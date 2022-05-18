using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour, IShooter
{
    [SerializeField] AbstractBullet bullet;
    [SerializeField] protected Transform heading;
    protected AbstractBullet lastBullet;

    public Transform Heading { get => heading; set => heading = value; }

    public virtual void init(AbstractShip ship)
    {

    }

    public void reload()
    {
    }

    public virtual void shoot()
    {
        print("shoot");
        lastBullet =  Instantiate(bullet, transform.position, transform.rotation);
    }
}

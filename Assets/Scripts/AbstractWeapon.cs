using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour, IShooter
{
    [SerializeField] protected Transform heading;
    [SerializeField] protected int damage;
    [SerializeField] protected float fireRate;
    protected bool canShoot = true;
    float fireRateTimer;

    public Transform Heading { get => heading; set => heading = value; }

    public virtual void init(AbstractShip ship)
    {

    }

    public void Update()
    {
        if (fireRateTimer > fireRate)
        {
            canShoot = true;
        }
        fireRateTimer += Time.deltaTime;
    }


    public void reload()
    {
        canShoot = false;
        fireRateTimer = 0;
    }

    public virtual void shoot()
    {
        reload();
    }
}

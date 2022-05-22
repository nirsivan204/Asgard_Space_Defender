using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour, IShooter
{
    [SerializeField] protected Transform heading;
    [SerializeField] protected int damage;
    [SerializeField] protected float fireRate;
    protected AbstractShip ship;
    protected bool canShoot = true;
    float fireRateTimer;
    protected GameMGR gameMGR;

    public Transform Heading { get => heading; set => heading = value; }

    public virtual void init(AbstractShip ship, GameMGR gameMGR)
    {
        this.ship = ship;
        this.gameMGR = gameMGR;
    }

    public void Update()
    {
        if (fireRateTimer > fireRate)
        {
            canShoot = true;
        }
        fireRateTimer += Time.deltaTime;
    }


    public void Reload()
    {
        canShoot = false;
        fireRateTimer = 0;
    }

    public virtual void Shoot()
    {
        Reload();
    }
}

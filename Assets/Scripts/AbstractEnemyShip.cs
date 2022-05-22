using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyShip : AbstractShip
{
    [SerializeField] protected float range;
    [SerializeField] protected PlayerShip TargetShip;



    protected override void FixedUpdate()
    {
        if (canMove)
        {
            CalculateNextMovement();
            if (isTargetNearby())
            {
                Aim();
                Shoot();

            }
            base.FixedUpdate();
        }
    }

    public void init(PlayerShip playerShip, GameMGR gameMGR, int health)
    {
        this.TargetShip = playerShip;
        this.gameMGR = gameMGR;
        base.init(health);

    }

    protected abstract void CalculateNextMovement();
    protected bool isTargetNearby()
    {
        return Vector3.Distance(transform.position, TargetShip.transform.position) <= range;
    }

    public override void Kill()
    {
        base.Kill();
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : AbstractShip
{
    [SerializeField] float range;
    [SerializeField] protected GameObject Target;

    protected void FixedUpdate()
    {
        //base.FixedUpdate();
        if (isTargetNearby())
        {
            Aim();
            //shoot();

        }
    }
    private void Start()
    {
        init(1);
    }
    private bool isTargetNearby()
    {
        return Vector3.Distance(transform.position, Target.transform.position) <= range;
    }

    protected override void Aim()
    {
        weapon.Heading.position = (weapon.transform.position + Target.transform.position) / 2;
    }

    public override void kill()
    {
        print("kill");
        base.kill();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : AbstractShip
{
    [SerializeField] float range;
    [SerializeField] protected PlayerShip TargetShip;
    Vector3 targetPos;
    [SerializeField] float offsetFactor;
    [SerializeField] private float avoidanceRadius;
    [SerializeField] private float avoidanceFactor;
    [SerializeField] private float targetProximityFactor;

    protected override void FixedUpdate()
    {
        CalculateNextMovement();
        if (isTargetNearby())
        {
            Aim();
            shoot();

        }
        base.FixedUpdate();

    }

    public void init(PlayerShip playerShip, int health)
    {
        this.TargetShip = playerShip;
        base.init(health);

    }

    protected override void Update()
    {
        base.Update();
    }

    private void CalculateNextMovement()
    {
        targetPos = TargetShip.transform.position + (transform.position - TargetShip.transform.position).normalized * offsetFactor ;
        print(targetPos);
        Vector3 AvoidanceForce =  CalculateObstacleAvoidanceForce();

        engineForce = ((targetPos - transform.position) * targetProximityFactor  + AvoidanceForce) *speed;
    }

    private Vector3 CalculateObstacleAvoidanceForce()
    {
        Vector3 totalForce = Vector3.zero;
        Collider[] collidersInRegion = Physics.OverlapSphere(transform.position, avoidanceRadius);
        foreach(Collider col in collidersInRegion)
        {
            if (!ReferenceEquals(col.gameObject, TargetShip.gameObject))
            {
                totalForce += (transform.position - col.transform.position) * avoidanceFactor;
            }
        }
        return totalForce;
    }

    private bool isTargetNearby()
    {
        return Vector3.Distance(transform.position, TargetShip.transform.position) <= range;
    }

    protected override void Aim()
    {
        weapon.Heading.position = (weapon.transform.position + TargetShip.transform.position) / 2;
    }

}

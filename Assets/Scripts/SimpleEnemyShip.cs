using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyShip : AbstractEnemyShip
{
    [SerializeField] float offsetFactor;
    [SerializeField] private float avoidanceRadius;
    [SerializeField] private float avoidanceFactor;
    [SerializeField] private float targetProximityFactor;

    protected override void CalculateNextMovement()
    {
        Vector3 targetPos = TargetShip.transform.position + (transform.position - TargetShip.transform.position).normalized * offsetFactor ;
        Vector3 AvoidanceForce =  CalculateObstacleAvoidanceForce();

        engineForce = ((targetPos - transform.position) * targetProximityFactor  + AvoidanceForce).normalized *speed;
    }

    private Vector3 CalculateObstacleAvoidanceForce()
    {
        Vector3 totalForce = Vector3.zero;
        Collider[] collidersInRegion = Physics.OverlapSphere(transform.position, avoidanceRadius);
        foreach(Collider col in collidersInRegion)
        {
            totalForce += (transform.position - col.transform.position) * avoidanceFactor;
        }
        return totalForce;
    }

    protected override void Aim()
    {
        weapon.Heading.position = (TargetShip.transform.position + weapon.transform.position)/2;
    }

}

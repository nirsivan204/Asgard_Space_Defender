using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    int damage;

    public void init(int damage)
    {
        this.damage = damage;
    }

    public void OnCollisonEnter(Collision other)
    {
        print("collide");
        IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (attribute != null)
        {
            attribute.hurt(damage);
        }
    }
}

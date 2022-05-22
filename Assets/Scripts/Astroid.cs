using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    int damage;
    public void init(int damage, float size)
    {
        this.damage = damage;
        this.transform.localScale *= size ;
    }

    public void OnCollisionEnter(Collision other)
    {
        print("collide");
        IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (attribute != null)
        {
            attribute.Hurt(damage);
        }
    }
}

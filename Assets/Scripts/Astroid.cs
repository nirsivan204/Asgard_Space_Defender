using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    int damage;
    [SerializeField] Rigidbody rb;
    public void init(int damage, float size, Vector3 velocity)
    {
        this.damage = damage;
        this.transform.localScale *= size ;
        rb.velocity = velocity;
    }

    public void OnCollisionEnter(Collision other)
    {
        IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (attribute != null)
        {
            attribute.Hurt(damage);
        }
    }
}

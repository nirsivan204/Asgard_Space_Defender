using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour // can be an inherited class of  abstract bullet, but not necesserily good practice
{
    int damage;
    [SerializeField] Rigidbody rb;
    protected float lifeSpan = 500;
    protected float timeLived;
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

    public void Update()
    {
        if (lifeSpan > 0 && timeLived >= lifeSpan)
        {
            Destroy(gameObject);
        }
        timeLived += Time.deltaTime;
    }
}

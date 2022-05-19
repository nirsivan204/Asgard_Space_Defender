using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{
    [SerializeField] int damage;
    protected float speed;
    protected Vector3 heading;
    protected float lifeSpan;
    protected float timeLived;
    //[SerializeField] float maxSpeed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] Transform weaponPosition;
    [SerializeField] GameObject shooter;

    public virtual void init(float bulletSpeed, Vector3 direction,GameObject shooter, int damage)
    {
        speed = bulletSpeed;
        heading = direction;
        if (rb)
        {
            rb.velocity = speed * direction.normalized;
        }
        timeLived = 0;
        this.shooter = shooter;
        this.damage = damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(shooter == null || !ReferenceEquals(other.gameObject,shooter))
        {
            IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
            if (attribute != null)
            {
                attribute.hurt(damage);
                print(attribute);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        //rb.AddForce(speed * heading);
        if (rb)
        {
            transform.LookAt(transform.position + rb.velocity);
        }
    }
}

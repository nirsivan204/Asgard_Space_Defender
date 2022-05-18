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

    public virtual void init(float bulletSpeed, Vector3 direction,GameObject shooter)
    {
        speed = bulletSpeed;
        heading = direction;
        rb.velocity = rb.velocity = speed * direction.normalized;
        timeLived = 0;
        this.shooter = shooter;
    }

    /*    public virtual void init(Vector3 heading, float startingSpeed)
        {
            heading = heading;
            speed = startingSpeed;
        }*/

    public void OnTriggerEnter(Collider other)
    {
        if(!ReferenceEquals(other.gameObject,shooter))
        {
            IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
            print("nir");
            if (attribute != null)
            {
                attribute.hurt(damage);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
            //rb.AddForce(speed * heading);

            transform.LookAt(transform.position + rb.velocity);
    }
}

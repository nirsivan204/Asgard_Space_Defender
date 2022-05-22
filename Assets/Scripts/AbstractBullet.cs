using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{
    [SerializeField] int damage;
    protected float speed;
    protected Vector3 heading;
    protected float lifeSpan = 100;
    protected float timeLived;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] Transform weaponPosition;
    [SerializeField] AbstractShip shooter;

    public virtual void init(float bulletSpeed, Vector3 direction, AbstractShip shooter, int damage)
    {
        speed = bulletSpeed;
        heading = direction;
        rb.velocity = speed * direction.normalized;
        timeLived = 0;
        this.shooter = shooter;
        this.damage = damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(shooter == null || !ReferenceEquals(other.gameObject,shooter.gameObject))
        {
            IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
            if (attribute != null)
            {
                attribute.Hurt(damage);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            transform.LookAt(transform.position + rb.velocity);
        }
    }

    protected void Update()
    {
        if (lifeSpan > 0 && timeLived >= lifeSpan)
        {
            Destroy(gameObject);
        }
        timeLived += Time.deltaTime;
    }
}

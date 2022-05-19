using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractShip : MonoBehaviour, IDamagable, IShooter
{
    protected float movementX;
    protected float movementY;
    protected float movementZ;
    protected int inversionFactor = 1;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float forwardSpeed;
    [SerializeField] protected AbstractWeapon weapon;
    [SerializeField] protected float turnRate;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected GameMGR gameMGR;
    public void heal(int amount)
    {
        if (health + amount > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;

        }
    }

    public void hurt(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            kill();
        }
    }

    public virtual void init(int health)
    {
        Aim();
        weapon.init(this);
        this.maxHealth = health;
        this.health = health;
    }

    public virtual void kill()
    {
        gameMGR.particleMGR.Play_Effect(ParticleMGR.ParticleTypes.Explosion,transform.position);
        print("kill");
        Destroy(gameObject);
    }

    public virtual void reload()
    {
    }

    public void shoot()
    {
        weapon.shoot();
    }

/*
    protected virtual void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY*inversionFactor);
        //if(rb.velocity.magnitude <= maxSpeed)
       // {
            rb.AddForce(movement * speed * rb.mass);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
       // }
//        rb.AddForce(movement * speed * rb.mass);

        //rb.AddForce(movement)
        //transform.Rotate(transform.forward, -movementX*turnRate);
        //transform.Rotate(transform.right, -movementY * turnRate);

        //transform.eulerAngles += -turnRate * Vector3.Cross(movementX*Vector3.right, transform.up);
        //transform.eulerAngles += -turnRate * Vector3.Cross(movementY *Vector3.forward, transform.up);

        transform.position += transform.up * forwardSpeed / 10;
    }*/
    protected abstract void Aim();
}

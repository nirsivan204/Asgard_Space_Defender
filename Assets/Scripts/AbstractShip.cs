using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractShip : MonoBehaviour, IDamagable, IShooter
{

    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float forwardSpeed;
    [SerializeField] protected AbstractWeapon weapon;
    protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected GameMGR gameMGR;
    [SerializeField] protected Transform mesh;
    [SerializeField] protected Transform heading;
    protected Vector3 engineForce;
    public UnityEvent OnKillEvent;
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

    protected virtual void FixedUpdate()
    {
        mesh.LookAt(heading);
    }

    protected void init(int health)
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
        OnKillEvent.Invoke();
        Destroy(gameObject);
    }

    public virtual void reload()
    {
    }

    public void shoot()
    {
        weapon.shoot();
    }

    protected virtual void Update()
    {
        rb.AddForce(engineForce);
        //print("forwardspeed = " + rb.velocity);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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

    public void OnCollisionEnter(Collision collision)
    {
        AbstractShip OtherShip = collision.gameObject.GetComponent<AbstractShip>();
        if (OtherShip != null)
        {
            OtherShip.kill();
            kill();
        }
    }
}

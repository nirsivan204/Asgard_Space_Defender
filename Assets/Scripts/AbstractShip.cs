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
    [SerializeField] protected AbstractWeapon weapon;
    protected bool canMove = false;
    protected int health;
    protected int maxHealth;
    [SerializeField] protected GameMGR gameMGR;
    [SerializeField] protected GameObject mesh;
    [SerializeField] protected Transform heading;
    protected Vector3 engineForce;
    public UnityEvent OnKillEvent;
    private bool isKilled = false;
    public void Heal(int amount)
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

    public virtual void Hurt(int amount)
    {
        health -= amount;
        gameMGR.musicMGR.Play_Sound(MusicMGR.SoundTypes.ShipHurt);
        if (health <= 0)
        {
            Kill();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (canMove)
        {
            mesh.transform.LookAt(heading);
        }
    }

    protected void init(int health)
    {
        weapon.init(this,gameMGR);
        this.maxHealth = health;
        this.health = health;
    }

    public virtual void Kill()
    {
        if (!isKilled)
        {
            isKilled = true;
            canMove = false;
            gameMGR.particleMGR.Play_Effect(ParticleMGR.ParticleTypes.Explosion, transform.position);
            gameMGR.musicMGR.Play_Sound(MusicMGR.SoundTypes.Boom);
            print("kill");
            OnKillEvent.Invoke();
        }

    }

    public virtual void Reload()
    {
    }

    public void Shoot()
    {
        weapon.Shoot();
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            rb.AddForce(engineForce);
            if(this.GetType() == typeof(PlayerShip)){
                print("forwardspeed = " + rb.velocity);
                print("speed = " + rb.velocity.magnitude);
            }
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
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
            OtherShip.Kill();
            Kill();
        }
    }

    public virtual void StartMoving()
    {
        canMove = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public virtual void StopMoving()
    {
        canMove = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}

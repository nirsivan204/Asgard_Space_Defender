using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : AbstractShip
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform mesh;
    [SerializeField] Transform heading;
    public void init(int health, PlayerController controller)
    {
        playerController = controller;
        inversionFactor = -1;
        playerController.fireEvent.AddListener(shoot);
        base.init(health);
    }

    protected void FixedUpdate()
    {
        movementY = playerController.movementY;
        movementX = playerController.movementX;
        movementZ = playerController.movementZ;
        heading.transform.localPosition = Vector3.MoveTowards(heading.transform.localPosition,new Vector3(movementX, heading.localPosition.y, movementY),Time.fixedDeltaTime*3);
        mesh.LookAt(heading);
/*        if(transform.position.magnitude > gameMGR.ArenaRadius)
        {
            transform.position = -transform.position;
        }*/
        //base.FixedUpdate();
    }
    float activeForwardSpeed;
    public float accel = 10;

    public void Update()
    {
        //mouseDistance = new Vector2(lookInput)
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, (1+movementZ) * speed, accel * Time.deltaTime);
        transform.Rotate(movementY * turnRate * Time.deltaTime, 0,-movementX * turnRate * Time.deltaTime, Space.Self);
        //print(movementZ);
        rb.AddForce(transform.up * activeForwardSpeed);
        //print("forwardspeed = " + rb.velocity);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

    }

    protected override void Aim()
    {
        weapon.Heading.position = weapon.transform.position + transform.up;
    }


    public void OnCollisonEnter(Collision other)
    {
        print("collide");
/*        IDamagable attribute = other.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (attribute != null)
        {
            attribute.hurt(damage);
        }*/
    }
}

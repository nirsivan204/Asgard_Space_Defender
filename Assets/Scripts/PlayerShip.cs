using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : AbstractShip
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float turnRate;
    float movementX;
    float movementY;
    float movementZ;
 //   int inversionFactor;

    public void init(int health, PlayerController controller)
    {
        playerController = controller;
        //inversionFactor = playerController.InversionFactor;
        playerController.fireEvent.AddListener(shoot);
        base.init(health);
    }

    protected override void FixedUpdate()
    {
        movementY = playerController.movementY;
        movementX = playerController.movementX;
        movementZ = playerController.movementZ;
        heading.transform.localPosition = Vector3.MoveTowards(heading.transform.localPosition,new Vector3(movementX, heading.localPosition.y, movementY),Time.fixedDeltaTime*3);
/*        if(transform.position.magnitude > gameMGR.ArenaRadius)
        {
            transform.position = -transform.position;
        }*/
        base.FixedUpdate();
    }
    protected float activeForwardSpeed;
    public float accel = 10;

    protected override void Update()
    {
        //mouseDistance = new Vector2(lookInput)
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, (1+movementZ) * speed, accel * Time.deltaTime);
        transform.Rotate(movementY * turnRate * Time.deltaTime, 0,-movementX * turnRate * Time.deltaTime, Space.Self);
        engineForce = transform.up * activeForwardSpeed;
        base.Update();
        //print(movementZ);

    }

    protected override void Aim()
    {
        weapon.Heading.position = weapon.transform.position + transform.up;
    }



}

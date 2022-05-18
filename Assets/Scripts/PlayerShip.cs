using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : AbstractShip
{
    [SerializeField] PlayerController playerController;
    public override void init(int health)
    {
        inversionFactor = -1;
        playerController.fireEvent.AddListener(shoot);
        base.init(health);
    }
    public void Start()
    {
        init(100);
    }
    protected override void FixedUpdate()
    {
        movementY = playerController.movementY;
        movementX = playerController.movementX;
        base.FixedUpdate();
    }

    protected override void Aim()
    {
        weapon.Heading.position = weapon.transform.position + transform.up;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : AbstractShip
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float turnRate;
    float movementX;
    float movementY;
    float movementZ;
    [SerializeField] float accel;
    private float activeForwardSpeed;
    [SerializeField] GameObject thrustEffect;
    [SerializeField] float thrustEffectScalingFactor;
    Vector3 thrustEffectStartingSize;

    public class UnityEventInt : UnityEvent<int> { };

    public UnityEventInt OnLivesChangedEvent { get; } = new UnityEventInt();

    public void init(int health, PlayerController controller)
    {
        base.init(health);
        playerController = controller;
        playerController.fireEvent.AddListener(Shoot);
        thrustEffectStartingSize = thrustEffect.transform.localScale;
        Aim();
    }

    protected override void FixedUpdate()
    {
        if (canMove)
        {
            movementY = playerController.movementY;
            movementX = playerController.movementX;
            movementZ = playerController.movementZ;
            heading.transform.localPosition = Vector3.MoveTowards(heading.transform.localPosition, new Vector3(movementX, heading.localPosition.y, movementY), Time.fixedDeltaTime * 3);
            base.FixedUpdate();
        }

    }

    protected override void Update()
    {
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, (1+movementZ) * speed, accel * Time.deltaTime);
        UpdateThrustEffect(movementZ);
        transform.Rotate(movementY * turnRate * Time.deltaTime, 0,-movementX * turnRate * Time.deltaTime, Space.Self);
        engineForce = transform.up * activeForwardSpeed;

        base.Update();
    }

    private void UpdateThrustEffect(float movementZ)
    {

        if (movementZ == 0)

        {
            thrustEffect.transform.localScale = thrustEffectStartingSize;

        }
        else
        {
            if (movementZ < 0 || !canMove)
            {
                thrustEffect.transform.localScale = Vector3.zero;
            }
            else
            {
                thrustEffect.transform.localScale = thrustEffectStartingSize * thrustEffectScalingFactor;

            }
        }
    }

    protected override void Aim()
    {
        weapon.Heading.position = weapon.transform.position + transform.up;
    }

    public override void Kill()
    {
        base.Kill();
        mesh.SetActive(false);
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void Hurt(int amount)
    {
        base.Hurt(amount);
        OnLivesChangedEvent.Invoke(this.health);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
{
    public float movementX;
    public float movementY;
    public UnityEvent fireEvent;
    public float movementZ;

    private void OnMove(InputValue movementValue)
    {
        Vector3 movementVector = movementValue.Get<Vector3>().normalized;
        movementX = movementVector.x;
        movementY = movementVector.y;
        movementZ = movementVector.z;
    }

    private void OnFire()
    {
        fireEvent.Invoke();
    }

    internal Vector2 getLookPosition()
    {
        throw new NotImplementedException();
    }
}

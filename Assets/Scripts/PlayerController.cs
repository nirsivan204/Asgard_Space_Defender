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
    public float movementZ;

    public UnityEvent fireEvent;
    public UnityEvent changePOVEvent;
   // int inversionFactor = -1;

    //public int InversionFactor { get => inversionFactor; set => inversionFactor = value; }

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

    private void OnChangePOV()
    {
        changePOVEvent.Invoke();
    }
}

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
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>().normalized;
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnFire()
    {
        fireEvent.Invoke();
    }
}

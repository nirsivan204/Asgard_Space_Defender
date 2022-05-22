using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController: AbstractController
{
    private void OnMove(InputValue movementValue)
    {
        Vector3 movementVector = movementValue.Get<Vector3>().normalized;
        base.OnMove(movementVector);
    }
}

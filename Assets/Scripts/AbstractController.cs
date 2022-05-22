using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractController : MonoBehaviour
{
    public float movementX;
    public float movementY;
    public float movementZ;
    public UnityEvent fireEvent;
    public UnityEvent changePOVEvent;


    protected virtual void OnFire()
    {
        fireEvent.Invoke();
    }
    protected virtual void OnChangePOV()
    {
        changePOVEvent.Invoke();
    }
    protected virtual void OnMove(Vector3 movementValues)
    {
        movementX = movementValues.x;
        movementY = movementValues.y;
        movementZ = movementValues.z;
    }

}

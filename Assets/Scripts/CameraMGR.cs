using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMGR : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ToggglePOV()
    {
        animator.SetTrigger("ChangeCamera");
    }
}

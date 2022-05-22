using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour
{
    [SerializeField] float lifeSpan;
    float timeLived;
    void Update()
    {
        if (lifeSpan > 0 && timeLived >= lifeSpan)
        {
            Destroy(gameObject);
        }
        timeLived += Time.deltaTime;
        
    }
}

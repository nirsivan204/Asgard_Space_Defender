using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour
{
    [SerializeField] float lifeSpan;
    float timeLived;

    // Update is called once per frame
    void Update()
    {
        if (timeLived >= lifeSpan)
        {
            Destroy(gameObject);
        }
        timeLived += Time.deltaTime;
        
    }
}

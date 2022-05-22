using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMGR : MonoBehaviour
{
    [Serializable]
    public class ParticleTypeAndRef
    {
        public ParticleTypes ParticleType;
        public GameObject particleRef;
    }

    [SerializeField]
    private List<ParticleTypeAndRef> ParticleType_And_Ref_List = new List<ParticleTypeAndRef>();


    public enum ParticleTypes
    {
        None,
        Explosion,

    }

    public GameObject PlayEffect(ParticleTypes particleType, Vector3 pos, Transform parent = null)
    {
        GameObject particle = getParticleSystemRef(particleType);

        return PlayEffect(particle, pos, parent);

    }

    private GameObject PlayEffect(GameObject particle, Vector3 position, Transform parent)
    {
        if (!parent)
        {
            parent = transform;
        }
        GameObject clone = Instantiate(particle, parent);
        clone.transform.localPosition = position;
        return clone;
    }

    private GameObject getParticleSystemRef(ParticleTypes particleType)
    {
        for (int i = 0; i < ParticleType_And_Ref_List.Count; i++)
        {
            if (ParticleType_And_Ref_List[i].ParticleType == particleType)
                return ParticleType_And_Ref_List[i].particleRef;
        }

        return null;
    }
}


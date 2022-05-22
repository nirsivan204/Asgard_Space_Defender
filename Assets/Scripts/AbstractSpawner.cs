using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractSpawner : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected float innerCreationRadius;
    [SerializeField] protected float creationRadius;
    [SerializeField] protected GameObject objectToSpawn;
    protected bool isSpawning = false;

    public void SpawnAroundPlayer(int numberOfElementToCreate, out List<GameObject> elements)
    {
        elements = new List<GameObject>();
        for(int i = 0; i < numberOfElementToCreate; i++)
        {
            Vector3 randomPos;
            do
            {
                randomPos = playerTransform.position + Random.insideUnitSphere * creationRadius;

            } while (Vector3.Distance(randomPos, playerTransform.position) < innerCreationRadius);
            GameObject clone = Instantiate(objectToSpawn, randomPos, Quaternion.Euler(Random.insideUnitSphere * 360));
            elements.Add(clone);
        }

    }
    public virtual void StopSpawning()
    {
        isSpawning = false;
    }

    public virtual void StartSpawning()
    {
        isSpawning = true;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMGR : AbstractSpawner
{
    [SerializeField] float distanceRequiredToReCreate;
    [SerializeField] int NumberOfAstroidsToCreate;
    [SerializeField] List<Vector3> createdAroundPosList;
    [SerializeField] float distanceRequiredToDelete;
    [SerializeField] float minimumVelocity;
    [SerializeField] float maximumVelocity;
    [SerializeField] int astroidDamage;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    public void Start()
    {
        if (distanceRequiredToDelete < creationRadius)
        {
            print("error: distanceRequiredToDelete < creationRadius");
        }
    }

    public void Update()
    {
                createdAroundPosList.RemoveAll(item =>
                    {
                        if (Vector3.Distance(playerTransform.position, item) <= distanceRequiredToDelete)
                        {
                            return false;
                        }
                        deleteAstroidsInRegion(item);
                        return true;
                    });

        foreach (Vector3 pos in createdAroundPosList)
        {
            if (Vector3.Distance(playerTransform.position, pos)< distanceRequiredToReCreate)
            {
                return;
            }

        }
        List<GameObject> astroidsCreated;
        SpawnAroundPlayer(NumberOfAstroidsToCreate,out astroidsCreated);
        foreach(GameObject astroid in astroidsCreated)
        {
            astroid.GetComponent<Astroid>().init(astroidDamage,Random.Range(minSize,maxSize));
        }
        createdAroundPosList.Add(playerTransform.position);
    }

    private void deleteAstroidsInRegion(Vector3 centerOfRegion)
    {
        Collider[] collidersInRegion = Physics.OverlapSphere(centerOfRegion, creationRadius);
        foreach (Collider item in collidersInRegion)
        {
            if (item.GetComponent<Astroid>() != null)
            {
                Destroy(item.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMGR : AbstractSpawner
{
    [SerializeField] float distanceRequiredToReCreate;
    [SerializeField] int NumberOfAstroidsToCreate;
    List<Vector3> createdAroundPosList = new List<Vector3>();
    [SerializeField] float distanceRequiredToDelete;
    float minimumVelocity;
    float maximumVelocity;
    int astroidDamage = int.MaxValue;
    float minSize;
    float maxSize;

    public void init(float minimumVelocity,float maximumVelocity,float minSize, float maxSize)
    {
        this.minimumVelocity = minimumVelocity;
        this.maximumVelocity = maximumVelocity;
        this.minSize = minSize;
        this.maxSize = maxSize;
    }


    public void Start()
    {
        if (distanceRequiredToDelete < creationRadius)
        {
            print("error: distanceRequiredToDelete < creationRadius");
        }
    }

    public void Update()
    {
        if (isSpawning) //in order to use less space, we will remove some of the astroids which are too far from the player, by saving in a list all the places we created around them
        {
            createdAroundPosList.RemoveAll(item =>
            {
                if (Vector3.Distance(playerTransform.position, item) <= distanceRequiredToDelete)
                {
                    return false;
                }
                deleteAstroidsInRegion(item);
                return true;
            }); //removing all positions of player we spawn around them and are too far (<distanceRequiredToDelete)

            foreach (Vector3 pos in createdAroundPosList) //if the player is near a position we already created around, dont need to create more
            {
                if (Vector3.Distance(playerTransform.position, pos) < distanceRequiredToReCreate)
                {
                    return;
                }

            }
            List<GameObject> astroidsCreated;
            SpawnAroundPlayer(NumberOfAstroidsToCreate, out astroidsCreated);
            foreach (GameObject astroid in astroidsCreated)
            {
                astroid.GetComponent<Astroid>().init(astroidDamage, Random.Range(minSize, maxSize), Random.Range(minimumVelocity,maximumVelocity)* Random.insideUnitSphere);
            }
            createdAroundPosList.Add(playerTransform.position); //add this player location to the list
        }
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

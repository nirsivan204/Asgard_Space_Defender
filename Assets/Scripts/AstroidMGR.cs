using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMGR : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] float creationRadius;
    [SerializeField] float innerCreationRadius;
    [SerializeField] float distanceRequiredToCreate;
    [SerializeField] int NumberOfAstroidsToCreate;
    [SerializeField] GameObject astroid;
    [SerializeField] List<Vector3> createdAroundPosList;
    [SerializeField] float distanceRequiredToDelete;
    public void createAstroidsAroundPlayer()
    {
        for(int i = 0; i < NumberOfAstroidsToCreate; i++)
        {
            Vector3 randomPos;
            do
            {
                randomPos = playerTransform.position + Random.insideUnitSphere * creationRadius;

            } while (Vector3.Distance(randomPos, playerTransform.position) < innerCreationRadius);
            Instantiate(astroid, randomPos, Quaternion.Euler(Random.insideUnitSphere));
        }
        createdAroundPosList.Add(playerTransform.position);

    }

    public void Start()
    {
        //createAstroidsAroundPlayer();
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
            if (Vector3.Distance(playerTransform.position, pos)< distanceRequiredToCreate)
            {
                return;
            }

        }
        createAstroidsAroundPlayer();
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

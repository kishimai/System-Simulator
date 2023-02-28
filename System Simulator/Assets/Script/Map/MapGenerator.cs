using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int numObjectsToSpawn;
    public float minDistance;
    public GameObject boundaryNorth, boundarySouth, boundaryEast, boundaryWest;
    [Space]
    public List<GameObject> objectsToSpawn = new List<GameObject>();
    private List<GameObject> objectsSpawned = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            GameObject obj = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
            Vector3 randomPos = GetRandomPosition();
            GameObject Obj = Instantiate(obj, randomPos, Quaternion.identity);
            objectsSpawned.Add(obj);
            Obj.transform.parent = this.transform;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos = Vector3.zero;
        bool validPosition = false;
        while (!validPosition)
        {
            float x = Random.Range(boundaryWest.transform.position.x, boundaryEast.transform.position.x);
            float y = Random.Range(boundarySouth.transform.position.y, boundaryNorth.transform.position.y);
            randomPos = new Vector3(x, y, 0f);
            validPosition = true;
            foreach (GameObject obj in objectsSpawned)
            {
                if (Vector3.Distance(randomPos, obj.transform.position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }
        }
        return randomPos;
    }
}
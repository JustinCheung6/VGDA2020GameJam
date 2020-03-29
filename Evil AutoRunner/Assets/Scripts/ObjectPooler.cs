using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Tooltip("Number of object ObjectPooler creates at start of game.")]
    [SerializeField] private int startingObjects = 20;

    private int totalObjects = 0;

    private List<GameObject> objectPool = null;

    //Object References
    [SerializeField] private GameObject pooledObjectPrefab = null;

    private void Awake()
    {
        fillObjectPool();
    }

    private void fillObjectPool()
    {
        //Reset and fill list with bullets
        objectPool = new List<GameObject>();
        for (int i = 0; i < startingObjects; i++)
        {
            MakeObject();
        }
    }
    private GameObject MakeObject()
    {
        GameObject obj = Instantiate(pooledObjectPrefab);
        //Set the object with the ObjectPooler script the parent of the bullets
        obj.transform.parent = transform;
        obj.SetActive(false);
        objectPool.Add(obj);

        totalObjects++;

        return obj;
    }

    //Return a bullet ready to be shot
    public GameObject GetBlock()
    {
        //Find an inactive bullet
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
                return objectPool[i];
        }

        //Add to the new bullet counter
        totalObjects++;
        //Return a newly made bullet
        return MakeObject();
    }

    private void OnApplicationQuit()
    {
        //Print out the number of bullets made after the start of the scene
        Debug.Log("There are " + (totalObjects - startingObjects) + " new bullets in the " + this.gameObject.name + " gameObject.");
    }
}

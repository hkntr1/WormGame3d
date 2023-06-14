using System.Collections.Generic;
using UnityEngine;

public class MagnetPool : Singleton<MagnetPool>
{
    public List<GameObject> prefab;
    public int maxObjects;

    private List<GameObject> availableObjects = new List<GameObject>();
    private List<GameObject> inUseObjects = new List<GameObject>();

    private void Start()
    {
        InitializeObjects();
    }

    private void InitializeObjects()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            int xPos = Random.Range(1, 99);
            int zPos = Random.Range(1, 99);
            GameObject obj = Instantiate(prefab[Random.Range(0, prefab.Count - 1)], new Vector3(xPos, 1, zPos), Quaternion.identity);
            obj.SetActive(true);
            inUseObjects.Add(obj);
            obj.transform.SetParent(transform);
            GroundController.Instance.cellControllers[xPos, zPos].fruits.Add(obj);
        }

    }

    public GameObject AcquireObject()
    {
        if (availableObjects.Count == 0)
        {
            return null;
        }

        GameObject obj = availableObjects[0];
        availableObjects.RemoveAt(0);
        inUseObjects.Add(obj);
        obj.SetActive(true);
        int xPos = Random.Range(1, 99);
        int zPos = Random.Range(1, 99);
        obj.transform.position = new Vector3(xPos, 1, zPos);
        GroundController.Instance.cellControllers[xPos, zPos].fruits.Add(obj);
        obj.transform.SetParent(transform);
        return obj;
    }

    public void ReleaseObject(GameObject obj)
    {
        if (inUseObjects.Contains(obj))
        {
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
            inUseObjects.Remove(obj);
            availableObjects.Add(obj);
        }
    }

    public void Cleanup()
    {
        for (int i = 0; i < availableObjects.Count; i++)
        {
            Destroy(availableObjects[i]);
        }
        availableObjects.Clear();
        inUseObjects.Clear();
    }

}
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<GameObject> prefab; // Havuzda tutulacak önceden oluþturulmuþ nesnenin prefab'ý
    public int maxObjects; // Havuzdaki maksimum nesne sayýsý

    private List<GameObject> availableObjects = new List<GameObject>(); // Kullanýlabilir nesnelerin listesi
    private List<GameObject> inUseObjects = new List<GameObject>(); // Kullanýmda olan nesnelerin listesi

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
            availableObjects.Add(obj);
            obj.transform.SetParent(transform);
            GroundController.Instance.cellControllers[xPos, zPos].fruits.Add(obj);
    }
     
    }

    public GameObject AcquireObject()
    {
        if (availableObjects.Count == 0)
        {
            // Havuzda kullanýlabilir nesne yoksa bekleyin veya isteðe baðlý olarak yeni bir nesne oluþturun
            // Örneðin: return Instantiate(prefab);
            return null;
        }

        GameObject obj = availableObjects[0];
        availableObjects.RemoveAt(0);
        inUseObjects.Add(obj);
        obj.SetActive(true);
        return obj;
    }

    public void ReleaseObject(GameObject obj)
    {
        if (inUseObjects.Contains(obj))
        {
            obj.SetActive(false);
            obj.transform.position = Vector3.zero; // Nesneyi baþlangýç pozisyonuna sýfýrlayabilirsiniz
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
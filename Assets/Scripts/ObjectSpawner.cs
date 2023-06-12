using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Oluþturulacak obje prefabý
    public int gridSize = 10; // Grid boyutu (100x100 için 10 kullanýyoruz)
    public float spacing = 1f; // Obje aralýðý
    public Transform spawnPoint; // Oluþturma noktasý

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 spawnPosition = spawnPoint.position + new Vector3(x * spacing, 0f, z * spacing);
                Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
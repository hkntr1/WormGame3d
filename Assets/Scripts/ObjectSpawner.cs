using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Olu�turulacak obje prefab�
    public int gridSize = 10; // Grid boyutu (100x100 i�in 10 kullan�yoruz)
    public float spacing = 1f; // Obje aral���
    public Transform spawnPoint; // Olu�turma noktas�

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayerSpawner : MonoBehaviour
{
    [SerializeField] int EnemyCount;
    [SerializeField] PlayerManager AIPrefab;
    private void Start()
    {
        for (int i = 0; i < EnemyCount; i++)
        {
            PlayerManager ai = Instantiate(AIPrefab,transform);
            PlaceAI(ai.transform);
        }
    }
    void PlaceAI(Transform ai)
    {
        ai.gameObject.SetActive(false);
        ai.GetChild(0).transform.position = CalculateSpawnPoint(ai.gameObject);
    }
    public Vector3 CalculateSpawnPoint(GameObject ai)
    {
        float minDistance = 20f;
        float maxDistance = 95f;
        float distance = Random.Range(minDistance, maxDistance);

        // Spawn noktasýnýn pozisyonunu al
        Vector3 spawnPoint = transform.position;

        // Rastgele bir yönde hareket ettir
        spawnPoint += Random.onUnitSphere * distance;
        spawnPoint.y = 0.5f;
        if (spawnPoint.z > 95)
        {
            spawnPoint.z = 90;
        }
        if (spawnPoint.x > 95)
        {
            spawnPoint.x = 90;
        }
        if (spawnPoint.z < 0)
        {
            spawnPoint.z = 10;
        }
        if (spawnPoint.x < 0)
        {
            spawnPoint.x = 10;
        }

        // Eðer spawnPoint hücrede bebek yoksa, spawnPoint'i geri döndür
        if (GroundController.Instance.cellControllers[(int)spawnPoint.x, (int)spawnPoint.z].babiesInCell.Count == 0)
        {
            ai.gameObject.SetActive(true);
            return spawnPoint;
        }
        else
        {
            Debug.Log("Recalculate");
            // Bebek varsa, tekrar hesaplamak için fonksiyonu özyinelemeli olarak çaðýr
            return CalculateSpawnPoint(ai);
        }
    }

}

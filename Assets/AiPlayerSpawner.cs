using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayerSpawner : MonoBehaviour
{
    [SerializeField] PlayerManager AIPrefab;
    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            PlayerManager ai = Instantiate(AIPrefab,transform);
            PlaceAI(ai.transform);
        }
    }
    void PlaceAI(Transform ai)
    {

        float minDistance = 20f;
        float maxDistance = 95f;
        float distance = Random.Range(minDistance, maxDistance);

      
        Vector3 spawnPoint = transform.position;

      
        spawnPoint += Random.onUnitSphere * distance;
        spawnPoint.y = 1f;
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

        if (GroundController.Instance.cellControllers[(int)spawnPoint.x,(int)spawnPoint.z].babiesInCell.Count>0)
        {
            PlaceAI(ai);
        }
        ai.GetChild(0).transform.position = spawnPoint;
    }
}

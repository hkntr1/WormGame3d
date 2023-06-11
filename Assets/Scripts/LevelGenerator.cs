using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{ 

    private GameObject cube;
    private Transform wallParent;
  
    private void Awake()
    {
       wallParent = transform;
       cube= Resources.Load<GameObject>("Prefabs/Cube");
    }
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel() 
    {
        for (int i = 0; i < 100; i++)
        {
            for (int k = 0; k < 100; k++)
            {
                if (k == 0 || i == 0 || i == 99 || k == 99)
                {
                    GameObject wall = Instantiate(cube, new Vector3(i, 0, k), Quaternion.identity);
                    wall.transform.SetParent(wallParent);
                }
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : Singleton<GroundController>
{
   public CellController cellController;
   public CellController[,] cellControllers;
    private void Awake()
    {
        cellControllers = new CellController[100,100];
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                CellController cell = Instantiate(cellController,new Vector3(i,0,j),Quaternion.identity);
                cell.gameObject.name = i + " " + j;
                cellControllers[i, j] =cell;
            }
        }
    }
}

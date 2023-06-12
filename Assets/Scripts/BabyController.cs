using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BabyController : MonoBehaviour
{
    public CellController currentCell;
   public void Die()
    {
        transform.parent.GetComponent<PlayerManager>().Die();
       
    }
    private void Start()
    {
        currentCell = GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z];
    }
    private void Update()
    {
        if (currentCell != GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z])
        {
            currentCell.babiesInCell.Remove(this);
            currentCell = GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z];
        }
        if (!currentCell.babiesInCell.Contains(this))
        {
            currentCell.babiesInCell.Add(this);
        }
        if (currentCell.babiesInCell.Count>0)
        {
            for (int i = 0; i < currentCell.babiesInCell.Count; i++)
            {
                if (currentCell.babiesInCell[i].transform.parent!=transform.parent)
                {
                    currentCell.babiesInCell[i].Die();
                }
            
            }
        }
    }
}

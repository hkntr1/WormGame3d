using UnityEngine;

public class BabyController : MonoBehaviour
{
    public CellController currentCell;
    private PlayerManager playerManager;


    public void Die()
    {
       playerManager.Die();

    }
    private void Start()
    {
        playerManager = transform.parent.GetComponent<PlayerManager>();
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
        if (currentCell.babiesInCell.Count > 0)
        {
            for (int i = 0; i < currentCell.babiesInCell.Count; i++)
            {
                if (currentCell.babiesInCell[i].transform.parent != transform.parent)
                {
                    if (playerManager.isAi)
                    {
                        currentCell.babiesInCell[i].Die();
                    }
                }

            }
        }
    }
}

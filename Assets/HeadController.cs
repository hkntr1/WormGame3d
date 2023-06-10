using UnityEngine;

public class HeadController : MonoBehaviour
{
    public bool isAi;
    private PlayerController playerController;
    private AIController AIController;
    private void Start()
    {
        if (isAi)
        {
            AIController = transform.parent.GetComponent<AIController>();
        }

        else
        {
            playerController = transform.parent.GetComponent<PlayerController>();
        }
    }
    void Update()
    {
        if (GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z].fruits.Count > 0)
        {
            if (isAi)
            {
               
                AIController.AddBodyPart();
            }
            else
            {
                playerController.AddBodyPart();

            }
            GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z].fruits[0].SetActive(false);
          
            GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z].fruits.Clear();
        }

    }
}

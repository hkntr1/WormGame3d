using System.Collections;
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
            foreach (GameObject item in GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z].fruits)
            {
                StartCoroutine(vacuumCaroutine(item));
         
            }
            GroundController.Instance.cellControllers[(int)transform.position.x, (int)transform.position.z].fruits.Clear();
        }

    }
    IEnumerator vacuumCaroutine(GameObject item)
    {
        item.transform.position = Vector3.Slerp(item.transform.position, transform.position, 0.5f);
        yield return new WaitForSeconds(0.4f);
        item.SetActive(false);
    }
}

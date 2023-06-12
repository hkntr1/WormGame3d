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
    void FixedUpdate()
    {
        int posX = (int)transform.position.x;
        int posY = (int)transform.position.y;
        if (posX > GroundController.Instance.cellControllers.GetLength(0) || posY > GroundController.Instance.cellControllers.GetLength(1) || posX <= 0 || posY <= 0)
        {
            Debug.Log("Died");
            if (isAi)
            {
                AIController.speed = 0;
            }
            else
            {
                playerController.speed = 0;
            }
            Destroy(transform.parent.gameObject, 1f);
            return;
        }
        for (int i = (int)transform.position.x - 2; i < (int)transform.position.x + 2; i++)
        {
            for (int k = (int)transform.position.z - 2; k < (int)transform.position.z + 2; k++)
            {
                if (GroundController.Instance.cellControllers[i, k].fruits.Count > 0)
                {
                    if (isAi)
                    {
                        AIController.AddBodyPart();
                    }
                    else
                    {
                        playerController.AddBodyPart();
                    }
                    foreach (GameObject item in GroundController.Instance.cellControllers[i, k].fruits)
                    {
                        StartCoroutine(vacuumCaroutine(item));
                    }
                    GroundController.Instance.cellControllers[i, k].fruits.Clear();
                }
            }
        }


    }
    IEnumerator vacuumCaroutine(GameObject item)
    {
        item.transform.SetParent(transform);
        StartCoroutine(LerpPosition(Vector3.zero, 0.7f, item.transform));
        // item.transform.localPosition = Vector3.Lerp(item.transform.localPosition,Vector3.zero, 2f*Time.smoothDeltaTime);
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.ReleaseObject(item);
        yield return new WaitForSeconds(1f);
        ObjectPool.Instance.AcquireObject();

    }
    IEnumerator LerpPosition(Vector3 targetPosition, float duration, Transform item)
    {
        float time = 0;
        Vector3 startPosition = item.localPosition;
        while (time < duration)
        {
            item.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

    }
}

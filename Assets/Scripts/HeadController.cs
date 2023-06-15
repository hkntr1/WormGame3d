using System.Collections;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    public bool isAi;
    private PlayerController playerController;
    private AIController AIController;
    PlayerManager playerManager;

    public int _area;
    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        if (isAi)
        {
            AIController = GetComponentInParent<AIController>();
        }

        else
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }
    void Update()
    {
        int posX = (int)transform.position.x;
        int posY = (int)transform.position.z;
        if (posX >= 98 || posY >= 98 || posX <= 0 || posY <= 0)
        {

            if (isAi)
            {
                //  AIController.speed = 0;
            }
            else
            {
                playerController.speed = 0;
            }
            playerManager.Die();
            return;
        }
        for (int i = (int)transform.position.x - _area; i < (int)transform.position.x + _area; i++)
        {
            for (int k = (int)transform.position.z - _area; k < (int)transform.position.z + _area; k++)
            {
                if (i < 0 || i > 99 || k < 0 || k > 99)
                {
                    Debug.Log("Alan Dýþý");
                }
                else
                {
                    if (GroundController.Instance.cellControllers[i, k].fruits.Count > 0)
                    {
                        foreach (GameObject item in GroundController.Instance.cellControllers[i, k].fruits)
                        {
                            if (!isAi)
                            {
                                if (item.CompareTag("magnet"))
                                {
                                    MagnetController.Instance.CoroutineStarter(this);
                                }
                                else
                                {
                                    ScoreManager.Instance.EatFood(transform);
                                }
                            }
                            playerManager.AddBeam();
                            StartCoroutine(vacuumCaroutine(item));
                        }
                        GroundController.Instance.cellControllers[i, k].fruits.Clear();
                    }
                }
            }
        }
    }

    IEnumerator vacuumCaroutine(GameObject item)
    {
        item.transform.SetParent(transform);
        StartCoroutine(LerpPosition(Vector3.zero, 0.5f, item.transform));
        StartCoroutine(LerpScale(0.5f, item.transform));
        // item.transform.localPosition = Vector3.Lerp(item.transform.localPosition,Vector3.zero, 2f*Time.smoothDeltaTime);
        if (!item.CompareTag("magnet"))
        {
            yield return new WaitForSeconds(0.5f);
            ObjectPool.Instance.ReleaseObject(item);
            yield return new WaitForSeconds(1f);
            ObjectPool.Instance.AcquireObject();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            MagnetPool.Instance.ReleaseObject(item);
            yield return new WaitForSeconds(1f);
            MagnetPool.Instance.AcquireObject();
        }


    }
    IEnumerator LerpScale(float duration, Transform item)
    {
        item.gameObject.SetActive(true);
        float time = 0;
        Vector3 startScale = item.localScale;
        item.localScale = startScale;
        while (time < duration)
        {

            item.localScale = Vector3.Lerp(startScale, startScale/3, time / duration);
            Debug.Log("item scale "+item.localScale);
            time += Time.deltaTime;
            yield return null;
        }
        item.gameObject.SetActive(false);
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

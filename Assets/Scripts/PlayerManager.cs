using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool isDead;
    int growthRate;
    int target = 5;
    int eatedCounter;
    private PlayerController playerController;
    private AIController AIController;
    public bool isAi;
    private int typeIndex;
    private void Start()
    {
        if (isAi)
        {
            AIController = transform.GetComponent<AIController>();
            Init();

        }
        else
        {
            playerController = transform.GetComponent<PlayerController>();
        }
    }
    public void Init()
    {
        typeIndex = Random.Range(1, 5);
        AIController.bodyPart = Resources.Load<GameObject>("Prefabs/body" + typeIndex);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Heads/face" + typeIndex);
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            AIController.AddBodyPart();
        }
    }
    public void AddBeam()
    {
        eatedCounter++;
        if (eatedCounter == target)
        {
            target = target * 2;
            eatedCounter = 0;
            playerController?.AddBodyPart();
            AIController?.AddBodyPart();
        }
    }

    public void Die()
    {
     
        if (!isAi&&!isDead)
        {
           
            Camera.main.transform.parent = null;
            ScoreManager.Instance.SaveScore();
            FindAnyObjectByType<AfterGameController>().gameObject.SetActive(true);
            FindAnyObjectByType<AfterGameController>().Init();
            transform.gameObject.SetActive(false);
        }

        if (!isDead)
        {
            isDead = true;
            transform.GetChild(0).gameObject.SetActive(false);

        }
        if (isAi)
        {
            Reborn();
        }

    }

    void Reborn()
    {
        transform.GetChild(0).transform.position = CalculateSpawnPoint();
  
        for (int i = 1; i < transform.childCount; i++)
        {
            Transform baby = transform.GetChild(i);
            AIController.Babies.Remove(baby);
            Destroy(transform.GetChild(i).gameObject);

        }
        for (int i = 0; i < Random.Range(0, 3); i++)
        {
            AIController.AddBodyPart();

        }
       
    }
    public Vector3 CalculateSpawnPoint()
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

        if (GroundController.Instance.cellControllers[(int)spawnPoint.x, (int)spawnPoint.z].babiesInCell.Count == 0)
        {

            StartCoroutine(GameObjectActiver());
            return spawnPoint;
        }
        else
        {
         
            return CalculateSpawnPoint();
        }
    }

    IEnumerator GameObjectActiver()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}

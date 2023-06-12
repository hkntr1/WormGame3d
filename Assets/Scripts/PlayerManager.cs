using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool isDead;
    int growthRate;
    int target=5;
    int eatedCounter;
    private PlayerController playerController;
    private AIController AIController;
    public bool isAi;
    private void Start()
    {
        if (isAi)
        {
            AIController = transform.GetComponent<AIController>();
        }

        else
        {
            playerController = transform.GetComponent<PlayerController>();
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
        if (!isDead)
        {
            isDead = true;
            transform.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool isDead;
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            transform.gameObject.SetActive(false);
        }
    }
}

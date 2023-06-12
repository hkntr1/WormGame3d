using UnityEngine;

public class Logaritma : MonoBehaviour
{
    public float initialTarget = 10f; // Ba�lang��taki hedef de�eri
    public float targetIncrement = 1f; // Her art��ta eklenecek hedef miktar�
    public float growthRate = 0.1f; // Hedef art���n�n ters logaritmik b�y�me oran�
    public float growthRateIncrement = 0.1f; // Ters logaritmik b�y�me oran�n�n art�� miktar�

    private float currentTarget; // Mevcut hedef de�eri
    private float previousTarget; // �nceki hedef de�eri

    private void Start()
    {
        currentTarget = initialTarget;
        previousTarget = currentTarget;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseTarget();
        }
    }

    private void IncreaseTarget()
    {
        float targetDifference = currentTarget - previousTarget;

        float increment = Mathf.Log(targetDifference + 1f, growthRate);
        float newTarget = previousTarget + increment * targetIncrement;

        previousTarget = currentTarget; // previousTarget g�ncelleniyor
        currentTarget = newTarget;

        // Ters logaritmik b�y�me oran�n� art�r
        growthRate += growthRateIncrement;

        Debug.Log("Yeni Hedef: " + currentTarget);
    }
}

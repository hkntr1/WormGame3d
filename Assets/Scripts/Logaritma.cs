using UnityEngine;

public class Logaritma : MonoBehaviour
{
    public float initialTarget = 10f; // Baþlangýçtaki hedef deðeri
    public float targetIncrement = 1f; // Her artýþta eklenecek hedef miktarý
    public float growthRate = 0.1f; // Hedef artýþýnýn ters logaritmik büyüme oraný
    public float growthRateIncrement = 0.1f; // Ters logaritmik büyüme oranýnýn artýþ miktarý

    private float currentTarget; // Mevcut hedef deðeri
    private float previousTarget; // Önceki hedef deðeri

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

        previousTarget = currentTarget; // previousTarget güncelleniyor
        currentTarget = newTarget;

        // Ters logaritmik büyüme oranýný artýr
        growthRate += growthRateIncrement;

        Debug.Log("Yeni Hedef: " + currentTarget);
    }
}

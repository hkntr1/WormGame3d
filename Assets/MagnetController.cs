using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class MagnetController : Singleton<MagnetController>
{
    [SerializeField] TextMeshProUGUI countText;
    private int duration=5;
    private void Start()
    {
        countText.transform.parent.gameObject.SetActive(false);
    }
    public void CoroutineStarter(HeadController headController)
    {
        StartCoroutine(MagnetCoroutine(headController));
    }
    IEnumerator MagnetCoroutine(HeadController headController)
    {
        countText.transform.parent.gameObject.SetActive(true);
        float time = 0;
 
        while (time < duration)
        {
            headController._area = 4;
            countText.text = (duration - (int)time).ToString();
            time += Time.deltaTime;
            yield return null;
        }
        countText.transform.parent.gameObject.SetActive(false);
        headController._area = 2;

    }
}

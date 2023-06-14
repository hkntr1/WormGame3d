using System.Collections;
using System.IO.Compression;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public void Init(Vector3 pos,int score)
    {
      
        transform.position =pos+Vector3.up;
        scoreText.text = score.ToString();
        if (score<0)
        {
            scoreText.color = Color.red;
            Debug.Log(score);
        }
  
        StartCoroutine(LerpScale(1f,transform));
   
    }
    IEnumerator LerpScale(float duration, Transform item)
    {
        item.gameObject.SetActive(true);
        float time = 0;
        Vector3 startScale = Vector3.one * 0.6f;
        transform.localScale=startScale;
        while (time < duration)
        {
        
            item.localScale = Vector3.Lerp(startScale, Vector3.one, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        item.gameObject.SetActive(false);

    }

}

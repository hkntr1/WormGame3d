using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Canvas worldCanvas;
    float comboTime = 2f;
    float comboTimer = 0f;
    public int eatedInASection;
    int score;
    bool isInaSection;
    
   [SerializeField] ScoreCanvasController scoreCanvasPrefab;

    
    public void EatFood(int foodValue,Transform headPos)
    {
        int scoreSection = Mathf.RoundToInt(Mathf.Log(foodValue) * 12);
        score +=scoreSection;
        eatedInASection = 0;
        scoreText.text = "Score: " + score;
      
        ScoreCanvasController scoreSectionCanvas = Instantiate(scoreCanvasPrefab,worldCanvas.transform);
        scoreSectionCanvas.Init(headPos.position,scoreSection);
    }
    public void EatFood(Transform head)
    {
        if (!isInaSection)
        {
            isInaSection = true;
            StartCoroutine(ComboCounter(head));
        }
        eatedInASection += 1;
    }
    IEnumerator ComboCounter(Transform head)
    {
        comboTimer = 0;
        while (comboTimer < comboTime)
        {
            comboTimer += Time.deltaTime;
            yield return null;
        }
        isInaSection = false;
        EatFood(eatedInASection,head);
    }
   
}

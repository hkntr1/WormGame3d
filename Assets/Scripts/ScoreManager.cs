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
    int TotalScore;
    bool isInaSection;
    
   [SerializeField] ScoreCanvasController scoreCanvasPrefab;

    private void Start()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore",0);
    }
    public void SaveScore()
    {
        TotalScore += score;
        PlayerPrefs.SetInt("TotalScore",TotalScore);
    }
    public int GetScore()
    {
        return score;
    }
    public void EatFood(int foodValue,Transform headPos)
    {
        int scoreSection = 0;
        if (foodValue<0)
        {
            scoreSection = foodValue;
        }
        else
        {
            scoreSection = Mathf.RoundToInt(Mathf.Log(foodValue) * 12);
        }
       
        score +=scoreSection;
        eatedInASection = 0;
        scoreText.text = "Score: " + score;
        Debug.Log(scoreSection+" SCORE IS") ;
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

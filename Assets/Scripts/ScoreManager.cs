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
        scoreText.text = "Score: " + score;
        TotalScore = PlayerPrefs.GetInt("TotalScore", 0);
    }
    public void SaveScore()
    {
        TotalScore += score;
        PlayerPrefs.SetInt("TotalScore", TotalScore);
    }
    public int GetScore()
    {
        return score;
    }
    public void EatFood(int foodValue, Transform headPos)
    {
        if (foodValue == 0)
        {
            return;
        }
        int scoreSection = 0;
        scoreSection = Mathf.RoundToInt((((foodValue * 1.7f) + 2)) * (((foodValue * 1.7f) + 2)));
        score += scoreSection;
        eatedInASection = 0;
        scoreText.text = "Score: " + score;

        ScoreCanvasController scoreSectionCanvas = Instantiate(scoreCanvasPrefab, worldCanvas.transform);
        scoreSectionCanvas.Init(headPos.position, scoreSection);

    }
    public void NitroPenalty(int penaltyScore, Transform headPos)
    {
        score += penaltyScore;
        scoreText.text = "Score: " + score;
        ScoreCanvasController scoreSectionCanvas = Instantiate(scoreCanvasPrefab, worldCanvas.transform);
        scoreSectionCanvas.Init(headPos.position, penaltyScore);
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
        EatFood(eatedInASection, head);
    }

}

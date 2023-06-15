using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterGameController :MonoBehaviour
{
 
    [SerializeField] TextMeshProUGUI score, TotalScore;
    public void Init()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        TotalScore.text = "Toplam Skor: " + PlayerPrefs.GetInt("TotalScore").ToString();
        score.text = "Skor: " + ScoreManager.Instance.GetScore().ToString();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    
    private int _mode;
    private TextMeshProUGUI modeButtonText;
    [SerializeField] TextMeshProUGUI TotalScoreText;
    [SerializeField] Image modeButton;
    [SerializeField] Image background;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        modeButtonText = modeButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        TotalScoreText.text = "SKOR: " + PlayerPrefs.GetInt("TotalScore");
        ModeSelector();
    }
   public void ModeSelector()
    {
        if (_mode == 0)
        {
            _mode = 1;
            modeButtonText.text = "KLASÝK";

        }
        else
        {
            _mode = 0;
            modeButtonText.text = "ARENA";
        }
        modeButton.sprite = Resources.Load<Sprite>("Buttons/" + _mode);
        background.sprite = Resources.Load<Sprite>("Sprites/" + _mode);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}

using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonHeld = false;
    private float holdTime = 0f;
    private float requiredHoldTime = 0.5f;
    PlayerController playerController;
    private int _penaltyTimer=2;
    private float _penaltyCounter;
    private void Start()
    {
        playerController=FindObjectOfType<PlayerController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;
        holdTime = 0f;
    }

    void Update()
    {
        if (isButtonHeld)
        {
            holdTime += Time.deltaTime;     
            if (holdTime >= requiredHoldTime)
            {
                _penaltyCounter += Time.deltaTime;
                if (_penaltyCounter>_penaltyTimer)
                {
                    _penaltyCounter = 0;
                    ScoreManager.Instance.EatFood(-15, playerController.Babies[0]);
                }
                playerController.nitroFactor = 3f;
            }
          
        }
        else
        {
            playerController.nitroFactor = 1f;
        }
       
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    private SpriteRenderer[] images;
    private void Start()
    {
        images = GetComponentsInChildren<SpriteRenderer>();
        ChangeVisual(PlayerPrefs.GetInt("Selected", 1));
    }
    public void ChangeVisual(int index)
    { 
        for (int i = 0; i < images.Length; i++)
        {
            if (i == 0)
            {
                images[i].sprite = Resources.Load<Sprite>("Sprites/Heads/face" + index);
            }
            else
            {
                images[i].sprite = Resources.Load<Sprite>("Sprites/Bodies/body" + index);
            }
        }
    }
}

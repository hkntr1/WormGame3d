using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormVisualController : MonoBehaviour
{
    public List<Image> images;
    public Image profile;
    private void Start()
    {
        ChangeVisual(PlayerPrefs.GetInt("Selected",1));
    }
    public void ChangeVisual(int index)
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (i==0)
            {
                images[i].sprite = Resources.Load<Sprite>("Sprites/Heads/face"+index);   
            }
            else
            {
                images[i].sprite = Resources.Load<Sprite>("Sprites/Bodies/body" + index);
            }
        }
        profile.sprite = Resources.Load<Sprite>("Sprites/Heads/face" + index);
        PlayerPrefs.SetInt("Selected",index);
    }
}

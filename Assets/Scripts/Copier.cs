using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Copier : MonoBehaviour
{
    public RectTransform handle;
    private RectTransform rectTransform;
    private Image Image;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Image = GetComponent<Image>();
    }
    void Update()
    {
        if (handle.localPosition == Vector3.zero)
        {
            Image.enabled = false;
            return;
        }
        Image.enabled = true;
        rectTransform.localRotation = handle.localRotation;
        rectTransform.localPosition = handle.localPosition;
    }
}

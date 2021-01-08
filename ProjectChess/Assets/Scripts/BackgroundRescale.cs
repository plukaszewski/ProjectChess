using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundRescale : MonoBehaviour
{
    Image bgImage;
    RectTransform rt;
    float aspectRatio;

    void Start()
    {
        bgImage = GetComponent<Image>();
        rt = bgImage.rectTransform;
        aspectRatio = bgImage.sprite.bounds.size.x / bgImage.sprite.bounds.size.y;
    }

    void Update()
    {
        if (!rt)
        {
            return;
        }

        if(Screen.height * aspectRatio >= Screen.width)
        {
            rt.sizeDelta = new Vector2(Screen.height * aspectRatio, Screen.height);
        }
        else
        {
            rt.sizeDelta = new Vector2(Screen.width, Screen.width / aspectRatio);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UInode : MonoBehaviour
{
    public Vector2 position;
    [SerializeField] private Sprite circleSprite;
    public RectTransform rectTransform;

    public UInode(Vector2 Givenposition) 
    {
        position = Givenposition;

        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

  

}

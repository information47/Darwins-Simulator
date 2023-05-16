using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        ShowGraph(new NeatNetwork(5, 0, 2, 1));
    }
    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    private void ShowGraph(NeatNetwork network)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100f;
        float inputSpacing = graphHeight / (network.InputNodes.Count -1);

        for (int i =0; i < network.InputNodes.Count; i++)
        {
            float yPosition = graphHeight - i * inputSpacing;
            
            CreateCircle(new Vector2(00, yPosition));
        }
    }
}
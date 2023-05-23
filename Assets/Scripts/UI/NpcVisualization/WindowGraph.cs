using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private float graphHeight;
    private float graphWidth;

    private List<GameObject> displayedNodes = new List<GameObject>();
    private List<GameObject> displayedConnections = new List<GameObject>();

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
    }

    public void ShowNetwork(NeatNetwork network)
    {
        // Supprimer les objets de neurones affichés précédemment
        foreach (GameObject nodeObject in displayedNodes)
        {
            Destroy(nodeObject);
        }
        displayedNodes.Clear();

        // Supprimer les objets de connexions affichés précédemment
        foreach (GameObject connectionObject in displayedConnections)
        {
            Destroy(connectionObject);
        }
        displayedConnections.Clear();

        Vector2[] nodePositions = GetNodesPositions(network);
        // shows nodes on graph
        for (int i = 0; i < network.Nodes.Count; i++)
        {
            CreateCircle(nodePositions[i]);
        }

        // shows connexions on graph
        for (int i =0; i < network.Connections.Count; i++)
        {
            if (network.Connections[i].isActive)
            {
                Vector2 fromPosition = nodePositions[network.Connections[i].inputNode];
                Vector2 toPosition = nodePositions[network.Connections[i].outputNode];

                CreateLine(fromPosition, toPosition, network.Connections[i].weight);
            }
        }
    }

    public void HideWindow()
    {
        gameObject.SetActive(false);
    }
    public void ShowWindow()
    {
        gameObject.SetActive(true);
    }

    public Vector2[] GetNodesPositions(NeatNetwork network)
    {
        Vector2[] positions = new Vector2[network.Nodes.Count]; 
        
        graphHeight = graphContainer.sizeDelta.y;
        graphWidth = graphContainer.sizeDelta.x;
        
        // space between nodes on Y axis
        float inputSpacing = graphHeight / (network.InputNodes.Count - 1);
        float outputSpacing = graphHeight / (network.OutputNodes.Count - 1);
        float hiddenSpacing = graphHeight / (network.HiddenNodes.Count - 1);
        
        int outputNumber = -1;
        int hiddenNumber = -1;

        for (int i = 0; i < network.Nodes.Count; i++)
        {
            // define the position for intputs nodes
            if (i < network.InputNodes.Count)
            {
                float yPosition = graphHeight - i * inputSpacing;
                float xPosition = 0;
                positions[i] = new Vector2(xPosition, yPosition);
            }
            
            // define the position for outputs nodes
            else if ( i < network.OutputNodes.Count + network.InputNodes.Count)
            {
                outputNumber++;
                float yPosition = graphHeight - outputNumber * outputSpacing;
                float xPosition = graphWidth;
                positions[i] = new Vector2(xPosition, yPosition);
            }

            // define th eposition for hidden nodes
            else
            {
                hiddenNumber++;
                float yPosition = graphHeight - hiddenNumber * hiddenSpacing;
                int offset = Random.Range(-80, +80);
                float xPostion = graphWidth / 2 + offset;
                positions[i] = new Vector2(xPostion, yPosition);
            }
        }
        return positions;
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        
        // get the circle shape
        gameObject.GetComponent<Image>().sprite = circleSprite;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        
        // circle size
        rectTransform.sizeDelta = new Vector2(20, 20);
        
        //define the minimun position into the graphContainer
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        displayedNodes.Add(gameObject);
    }

    private void CreateLine(Vector2 startPosition, Vector2 endPosition, float weight)
    {
        // define the line color depending on the weight sign.
        Color lineColor = weight >= 0 ? Color.green : Color.red;
        
        // define the color intensity depending on the weight grandeur
        float colorIntensity = Mathf.Abs(weight);
        lineColor *= colorIntensity;
        
        // crete a line
        GameObject lineObject = new GameObject("Line", typeof(Image));
        lineObject.transform.SetParent(graphContainer, false);
        Image lineImage = lineObject.GetComponent<Image>();
        lineImage.color = lineColor;
        
        Vector2 difference = endPosition - startPosition;
        
        // Line center position
        RectTransform lineRectTransform = lineObject.GetComponent<RectTransform>();
        lineRectTransform.anchoredPosition = startPosition + difference * 0.5f;
        
        // line width
        lineRectTransform.sizeDelta = new Vector2(difference.magnitude, 6f);
        
        lineRectTransform.anchorMin = new Vector2(0, 0);
        lineRectTransform.anchorMax = new Vector2(0, 0);


        // orientation
        if (difference.magnitude > 0.001f) // eviter les divisions par 0
        {
            lineRectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
        }
        else
        {
            // Aucune rotation si la ligne est très courte
            lineRectTransform.localEulerAngles = Vector3.zero;
        }

        displayedConnections.Add(lineObject);
    }
}
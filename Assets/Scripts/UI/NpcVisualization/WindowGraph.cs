using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private float graphHeight;
    private float graphWidth;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        ShowNetwork(new NeatNetwork(5, 2, 0, 1));
  

    }
    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject UInode = new("Circle", typeof(Image));
        UInode.transform.SetParent(graphContainer, false);
        UInode.GetComponent<Image>().sprite = circleSprite;
        
        RectTransform rectTransform = UInode.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    private void ShowNetwork(NeatNetwork network)
    {
        graphHeight = graphContainer.sizeDelta.y;
        graphWidth = graphContainer.sizeDelta.x;

        NeatGenome genome = network.MyGenome;
        List<NodeGene> nodes = genome.NodeGenes;
        List<ConGene> connexions = genome.ConGenes;

        // ----- Shows nodes on graph -----

        float inputSpacing = graphHeight / (network.InputNodes.Count - 1);
        float outputSpacing = graphHeight / (network.OutputNodes.Count - 1);
        
        int outputNumber = -1;
        
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Type == NodeGene.TYPE.Input)
            {
                // define the position on Y axis for intputs nodes
                float yPosition = graphHeight - i * inputSpacing;
                float xPosition = 0;

                CreateCircle(new Vector2(xPosition, yPosition));
            }

            if (nodes[i].Type == NodeGene.TYPE.Output)
            {
                outputNumber++;
                
                // define the position on Y axis for outputs nodes
                float yPosition = graphHeight - outputNumber * outputSpacing;
                float xPosition = graphWidth;

                CreateCircle(new Vector2(xPosition, yPosition));
            }
        }

        // ----- Shows connections -----
        foreach(ConGene con in connexions)
        {

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Node : MonoBehaviour
{
    public int id;
    public float value;
    public List<Connection> inputConnections;
    public List<Connection> outputConnections;
    private IActivation nodeActivation;


    public Node(int ident)
    {
        id = ident;
        inputConnections = new List<Connection>();
        outputConnections = new List<Connection>();
    }

    public void SetNodeActivation(IActivation act)
    {
        nodeActivation = act;
    }
    
    
    public void SetInputNodeValue(float input)
    {
        value = this.nodeActivation.DoActivation(input);
    }
    
    public void SetNodeValue()
    {
        float val = 0;
        foreach (Connection con in inputConnections)
        {
            val += (con.weight * con.inputNodeValue);
        }
        value = this.nodeActivation.DoActivation(val);

    }

    public void FeedForwardValue()
    {
        foreach (Connection con in outputConnections)
        {
            con.inputNodeValue = value;
        }
    }
    
}
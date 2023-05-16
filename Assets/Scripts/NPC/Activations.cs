using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activations
{
    public List<actFunc> functions;
    public delegate float actFunc(float x);



    public Activations()
    {
        functions = new List<actFunc>();
        functions.Add(Sigmoid);
        functions.Add(TanH);
        functions.Add(TanHMod1);
        functions.Add(Step);
        functions.Add(Relu);
    }
    public float Sigmoid(float x)
    {
        return (float)(1 / (1 + Mathf.Exp(-x)));
    }

    public float Step(float x)
    {
        if (x < 0.0) return 0f;
        else return 1f;
    }

    public float Relu(float x)
    {
        if (x <= 0f) return 0f;
        else return x;
    }

    public float TanH(float x)
    {
        return ((2 / (1 + Mathf.Exp(-2 * x))) - 1);
    }

    public float TanHMod1(float x)
    {
        return ((2 / (1 + Mathf.Exp(-4 * x))) - 1);
    }
}

public class Sigmoid : IActivation
{
    public float DoActivation(float x)
    {
        return (float)(1 / (1 + Mathf.Exp(-x)));
    }
}

public class TanH : IActivation
{
    public float DoActivation(float x)
    {
        return ((2 / (1 + Mathf.Exp(-2 * x))) - 1);
    }
}

public class TanHMod1 : IActivation
{
    public float DoActivation(float x)
    {
        return ((2 / (1 + Mathf.Exp(-4 * x))) - 1);
    }
}
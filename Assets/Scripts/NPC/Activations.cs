using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
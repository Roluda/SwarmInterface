using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Distribution : MonoBehaviour
{
    public abstract float Value(float x);
    public abstract float Maximum
    {
        get;
    }
    public abstract float LowerBound
    {
        get;
    }
    public abstract float UpperBound
    {
        get;
    }
    public abstract event UnityAction ValueChanged;
}
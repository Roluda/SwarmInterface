using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IFunctionCurve
{
    float Value(float x);
    event UnityAction ValueChanged;
    float Maximum
    {
        get;
    }
}

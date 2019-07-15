using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// creates a pair of normal distributed random variables according to the Marsaglia Polar Method
/// </summary>
public class MarsagliaGenerator
{
    public static float Next()
    {
        Vector2 validDouble = Random.insideUnitCircle;
        float s = validDouble.sqrMagnitude;
        return validDouble.x * Mathf.Sqrt((-2 * Mathf.Log(s)) / s);
    }
}

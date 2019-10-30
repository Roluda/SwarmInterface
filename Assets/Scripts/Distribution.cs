using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// this is an abstract class for all distributions that need to be rendered on screen
/// </summary>
public abstract class Distribution : MonoBehaviour
{
    /// <summary>
    /// returns the probability P(x)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public abstract float Value(float x);

    /// <summary>
    /// the highest probable event
    /// </summary>
    public abstract float Maximum
    {
        get;
    }
    /// <summary>
    /// where the values start to become existent or interesting
    /// </summary>
    public abstract float LowerBound
    {
        get;
    }
    /// <summary>
    /// where the values stop being existent or interesting
    /// </summary>
    public abstract float UpperBound
    {
        get;
    }

    /// <summary>
    /// this is called when there is a change in the distribution
    /// </summary>
    public abstract event UnityAction ValueChanged;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalDistribution: Distribution
{
    public override event UnityAction ValueChanged;

    float _mean = 0;
    float _standardDeviation = 1;
    public float Mean
    {
        get
        {
            return _mean;
        }
        set
        {
            _mean = value;
            ValueChanged?.Invoke();
        }
    }

    public float StandardDeviation
    {
        get
        {
            return _standardDeviation;
        }
        set
        {
            _standardDeviation = value;
            ValueChanged?.Invoke();
        }
    }

    public override float Maximum
    {
        get
        {
            return Value(Mean);
        }
    }

    public override float LowerBound
    {
        get
        {
            return Mean - StandardDeviation * 4;
        }
    }

    public override float UpperBound
    {
        get
        {
            return Mean + StandardDeviation * 4;
        }
    }
    public override float Value(float x)
    {
        return (1 / (StandardDeviation * Mathf.Sqrt(2 * Mathf.PI))) * Mathf.Exp(-0.5f * Mathf.Pow((x - Mean) / StandardDeviation, 2));
    }
}

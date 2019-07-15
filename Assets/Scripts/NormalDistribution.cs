using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDistribution
{
    public delegate void DistributionChangedAction();
    public event DistributionChangedAction OnDistributionChange;

    public NormalDistribution(float expectation, float standardDeviation)
    {
        Mean = expectation;
        StandardDeviation = standardDeviation;
    }

    float _mean;
    float _standardDeviation;
    public float Mean
    {
        get
        {
            return _mean;
        }
        set
        {
            _mean = value;
            OnDistributionChange?.Invoke();
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
            OnDistributionChange?.Invoke();
        }
    }

    public float GetValue(float x)
    {
        return (1 / (StandardDeviation * Mathf.Sqrt(2 * Mathf.PI))) * Mathf.Exp(-0.5f * Mathf.Pow((x - Mean) / StandardDeviation, 2));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArithmeticDistribution : NormalDistribution
{
    [SerializeField]
    ParticleManager particleManager;

    void Update()
    {
        Mean = particleManager.PositionAverage;
        StandardDeviation = particleManager.PositionStandardDeviation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionTester : MonoBehaviour
{
    [SerializeField]
    ParticleManager distribution;
    [SerializeField]
    float mean;
    [SerializeField]
    float deviation;
    [SerializeField]
    float variance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mean = distribution.PositionAverage;
        variance = distribution.PositionVariance;
        deviation = distribution.PositionStandardDeviation;
    }
}

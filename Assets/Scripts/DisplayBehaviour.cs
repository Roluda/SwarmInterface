using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    ParticleManager particleManager;
    [SerializeField]
    WinCondition winCondition;
    [SerializeField]
    Visualizer distributedParticlesVisualizer;
    [SerializeField]
    Visualizer targetDistributionVisualizer;
    [SerializeField]
    TMP_Text mean;
    [SerializeField]
    TMP_Text stdDev;

    // Start is called before the first frame update
    public void Start()
    {
        distributedParticlesVisualizer.ObservedDistribution = particleManager.distribution;
        targetDistributionVisualizer.ObservedDistribution = winCondition.distribution;
    }

    // Update is called once per frame
    void Update()
    {
        mean.text = System.Math.Round(particleManager.PositionAverage, 3).ToString();
        stdDev.text = System.Math.Round(particleManager.PositionStandardDeviation, 3).ToString();
        AdaptWindow();
    }

    void AdaptWindow()
    {
        float vertical = Mathf.Max(particleManager.distribution.GetValue(particleManager.distribution.Mean), winCondition.distribution.GetValue(winCondition.distribution.Mean)) * 1.1f;
        float horizontal = Mathf.Max((particleManager.distribution.Mean + winCondition.distribution.Mean) * 2,10);
        float center = (particleManager.distribution.Mean + winCondition.distribution.Mean) / 2;
        //distributedParticlesVisualizer.HorizontalScale = horizontal;
        distributedParticlesVisualizer.VerticalScale = vertical;
        //targetDistributionVisualizer.HorizontalScale = horizontal;
        targetDistributionVisualizer.VerticalScale = vertical;
        distributedParticlesVisualizer.Center = center;
        targetDistributionVisualizer.Center = center;
    }
}

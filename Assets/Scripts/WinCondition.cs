using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// creates a random normal distribution as target for the particles on Start or when a low KL-Divergence is met
/// </summary>
public class WinCondition : MonoBehaviour
{
    [SerializeField]
    Vector2 targetMeanRange;
    [SerializeField]
    Vector2 targetStandardDeviationRange;
    [SerializeField]
    KullbackLeiblerDivergence KLDiv;
    public GameObject targetCurve;
    public NormalDistribution normalDistribution;
    // Start is called before the first frame update
    void Awake()
    {
        targetCurve = normalDistribution.gameObject;
        KLDiv.lowDivergence += NewGoal;
    }

    void Start()
    {
        NewGoal();
    }

    public void NewGoal()
    {
        normalDistribution.Mean = Random.Range(targetMeanRange.x, targetMeanRange.y);
        normalDistribution.StandardDeviation = Random.Range(targetStandardDeviationRange.x, targetStandardDeviationRange.y);
        targetCurve = normalDistribution.gameObject;
    }
}

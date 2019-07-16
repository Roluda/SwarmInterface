using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    KullbackLeiblerDivergence KLD;
    [SerializeField]
    float winningKL;
    [SerializeField]
    Vector2 targetMeanRange;
    [SerializeField]
    Vector2 targetStandardDeviationRange;

    public GameObject targetCurve;
    public NormalDistribution normalDistribution;
    // Start is called before the first frame update
    void Awake()
    {
        KLD.lowDivergence += NewGoal;
        KLD.lowDivergenceRange = winningKL;
        targetCurve = normalDistribution.gameObject;
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

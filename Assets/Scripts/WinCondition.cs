using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

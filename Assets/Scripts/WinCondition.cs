using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    Vector2 targetMeanRange;
    [SerializeField]
    Vector2 targetStandardDeviationRange;

    public NormalDistribution distribution = new NormalDistribution(0, 1);
    // Start is called before the first frame update
    void Start()
    {
        NewGoal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGoal()
    {
        distribution.Mean = Random.Range(targetMeanRange.x, targetMeanRange.y);
        distribution.StandardDeviation = Random.Range(targetStandardDeviationRange.x, targetStandardDeviationRange.y);
    }
}

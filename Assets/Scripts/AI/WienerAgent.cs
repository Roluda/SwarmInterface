using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class WienerAgent : Agent
{
    public ParticleManager particles;
    public NormalDistribution target;
    public KullbackLeiblerDivergence kl;

    void Start()
    {
        kl.lowDivergence += Done;
    }

    public override void CollectObservations()
    {
        AddVectorObs(particles.PositionAverage);
        AddVectorObs(particles.PositionStandardDeviation);
        AddVectorObs(target.Mean);
        AddVectorObs(target.StandardDeviation);
        //AddVectorObs(particles.RelativeDrift);
        AddVectorObs(particles.AbsoluteDrift);        
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        particles.RelativeDrift = vectorAction[0];
        particles.AbsoluteDrift += vectorAction[1] * Time.deltaTime;
        AddReward(-kl.Divergence);
    }

    public override void AgentReset()
    {
        Debug.Log("AgentReset");
    }
}

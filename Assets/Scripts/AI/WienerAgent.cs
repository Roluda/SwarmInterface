using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class WienerAgent : Agent
{
    public ParticleManager particles;
    public NormalDistribution target;
    public KullbackLeiblerDivergence kl;

    bool hitGoal;

    void Start()
    {
        kl.lowDivergence += () => hitGoal = true; 
    }

    public override void CollectObservations()
    {
        AddVectorObs(target.Mean - particles.PositionAverage);
        AddVectorObs(target.StandardDeviation - particles.PositionStandardDeviation);
        //AddVectorObs(kl.Divergence);
        //AddVectorObs(particles.RelativeDrift);
        //AddVectorObs(particles.AbsoluteDrift);        
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        particles.RelativeDrift = vectorAction[0];
        particles.AbsoluteDrift = vectorAction[1];// * Time.deltaTime;
        if (hitGoal)
        {
            SetReward(1);
            Done();
        }
    }

    public override void AgentReset()
    {
        hitGoal = false;
        Debug.Log("AgentReset");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class WienerAcademy : Academy
{
    [SerializeField]
    ParticleManager particleManager;
    [SerializeField]
    WinCondition winCondition;
    [SerializeField]
    KullbackLeiblerDivergence klDiv;

    public override void AcademyReset()
    {
        klDiv.lowDivergenceRange = resetParameters["win_condition"];
        particleManager.Setup();
        winCondition.NewGoal();
    }
}

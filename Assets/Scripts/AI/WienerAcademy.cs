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
    public override void AcademyReset()
    {
        particleManager.Setup();
        winCondition.NewGoal();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

/// <summary>
/// creates a random normal distributed number using marsaglia polar method
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    ParticleManager particleManager;
    [SerializeField]
    WienerAgent agent;
    [SerializeField]
    Brain playerBrain;
    [SerializeField]
    Brain aiBrain;
    bool isAi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(isAi);
            isAi = !isAi;
            if (!isAi) {
                agent.brain = playerBrain;
            }
            else
            {
                agent.brain = aiBrain;
            }

        }
    }
}

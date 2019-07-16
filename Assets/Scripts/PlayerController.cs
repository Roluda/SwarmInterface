using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    ParticleManager particleManager;

    // Update is called once per frame
    void Update()
    {
        particleManager.AbsoluteDrift += Input.GetAxis("Horizontal")*Time.deltaTime;
        particleManager.RelativeDrift = Input.GetAxis("Vertical");
    }
}

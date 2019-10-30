using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A particle
/// </summary>
public class Particle
{
    public Particle(float x)
    {
        X = x;
    }
    //the position of the particle
    private float x;
    public float X
    {
        get
        {
            return x;
        }
        set
        {
            if (!float.IsNaN(value))
            {
                x = value;
            }
        }
    }
}

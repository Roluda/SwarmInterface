﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour, IPlottable
{
    public Particle[] particles;

    #region serializeFields

    [SerializeField]
    int initialParticles;
    [SerializeField]
    float initialMean;
    [SerializeField]
    float initialStandardDeviation;
    [SerializeField]
    float relativeDriftWeight;
    [SerializeField]
    float absoluteDriftWeight;
    [SerializeField]
    float diffuse;

    #endregion

    #region properties

    /// <summary>
    /// return the average Position of the particles
    /// </summary>
    public float PositionAverage
    {
        get
        {
            float result = 0;
            foreach(Particle p in particles)
            {
                result += p.X;
            }
            result = result / particles.Length;
            if (!float.IsNaN(result))
            {
                return result;
            }
            else
            {
                Debug.Log("Average was not a Number");
                return 0;
            }
        }
    }


    /// <summary>
    /// returns the variance of the particles position
    /// </summary>
    public float PositionVariance
    {
        get
        {
            float average = PositionAverage;
            float squaredErrorSum = 0;
            foreach(Particle p in particles)
            {
                squaredErrorSum += Mathf.Pow(p.X - average, 2);
            }
            return squaredErrorSum / (particles.Length);
        }
    }

    /// <summary>
    /// returns the standard deviation of the particls position
    /// </summary>
    public float PositionStandardDeviation
    {
        get
        {
            return Mathf.Sqrt(PositionVariance);
        }
    }

    /// <summary>
    /// returns the lower Bound of the Particles
    /// </summary>
    public float PositionMinimum
    {
        get
        {
            float min = 0;
            foreach(Particle p in particles)
            {
                if (p.X < min) min = p.X;
            }
            return min;
        }
    }

    /// <summary>
    /// returns the upper Bound of the Particles
    /// </summary>
    public float PositionMaximum
    {
        get
        {
            float max = 0;
            foreach(Particle p in particles)
            {
                if (p.X > max) max = p.X;
            }
            return max;
        }
    }
    private float relativeDrift;
    public float RelativeDrift
    {
        get
        {
            return relativeDrift;
        }
        set
        {
            relativeDrift = value;
        }
    }
    private float absoluteDrift;
    public float AbsoluteDrift
    {
        get
        {
            return absoluteDrift;
        }
        set
        {
            absoluteDrift = value;
        }
    }

    #endregion

    #region monobehaviour callbacks

    public void Start()
    {
        Setup();
    }

    public void Update()
    {
        foreach(Particle p in particles)
        {
            ApplyDrift(p);
        }
    }

    #endregion

    public void Setup()
    {
        AbsoluteDrift = 0;
        particles = new Particle[initialParticles];
        for (int i = 0; i < initialParticles; i++)
        {
            float normalDistributedRandomNumber = MarsagliaGenerator.Next() * initialStandardDeviation + initialMean;
            particles[i] = new Particle(normalDistributedRandomNumber);
        }
    }

    public void ApplyDrift(Particle particle)
    {
        particle.X += (particle.X * RelativeDrift * relativeDriftWeight + AbsoluteDrift * absoluteDriftWeight) * Time.deltaTime + diffuse * MarsagliaGenerator.Next() * Time.deltaTime;
    }

    Dictionary<string, float> IPlottable.Data()
    {
        Dictionary<string, float> data = new Dictionary<string, float>();
        data.Add("K",RelativeDrift * relativeDriftWeight);
        data.Add("C",AbsoluteDrift * absoluteDriftWeight);
        return data;
    }
}

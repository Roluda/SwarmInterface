using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// creates a Histogramm of the particles of an observed particleManager
/// </summary>
public class Histogram : Distribution
{
    [SerializeField]
    ParticleManager observedParticles;
    [SerializeField]
    int resolution; //how many segments the histogram has
    float lowerBound; //where the histogram starts
    float upperBound; //where the histogram ends

    public override event UnityAction ValueChanged;

    Dictionary<int,float> values;
    float range;

    public override float Maximum //see Distribution.cs
    {
        get
        {
            float max = 0;
            foreach (KeyValuePair<int, float> kvp in values)
            {
                if (kvp.Value >= max)
                {
                    max = kvp.Value;
                }
            }
            return (max / observedParticles.particles.Length) / (range / resolution);
        }
    }

    public override float LowerBound //see Distribution.cs
    {
        get
        {
            return lowerBound;
        }
    }

    public override float UpperBound //see Distribution.cs
    {
        get
        {
            return upperBound;
        }
    }


    #region UnityCallbacks

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Observe();
    }

    #endregion

    /// <summary>
    /// observes the particle Manager
    /// </summary>
    void Observe()
    {
        lowerBound = observedParticles.PositionMinimum;
        upperBound = observedParticles.PositionMaximum;
        range = upperBound - lowerBound;
        Dictionary<int,float> updatedValues = new Dictionary<int, float>();
        foreach(Particle p in observedParticles.particles)
        {
            if(p.X>=lowerBound && p.X <=upperBound)
            {
                int segment = Mathf.FloorToInt((p.X/range) *resolution); //sorts the particles into segments of the histogram
                if (!updatedValues.ContainsKey(segment))
                {
                    updatedValues.Add(segment, 1f);
                }
                else
                {
                    updatedValues[segment]++;
                }
            }
        }
        values = updatedValues;
        ValueChanged?.Invoke();
    }

    public override float Value(float x)
    {
        if(x<lowerBound || x > upperBound)
        {
            return 0;
        }
        else
        {
            int segment = Mathf.FloorToInt((x/range) * resolution);
            if (values.ContainsKey(segment))
            {
                return (values[segment] / observedParticles.particles.Length) / (range/resolution); //relative probabilty
            }
            else
            {
                return 0;
            }
        }
    }
}

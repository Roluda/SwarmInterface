using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Histogram : Distribution
{
    [SerializeField]
    ParticleManager observedParticles;
    [SerializeField]
    int resolution;
    float lowerBound;
    float upperBound;

    public override event UnityAction ValueChanged;

    Dictionary<int,float> values;
    float range;

    public override float Maximum
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

    public override float LowerBound
    {
        get
        {
            return lowerBound;
        }
    }

    public override float UpperBound
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
                int segment = Mathf.FloorToInt((p.X/range) *resolution);
                if (!updatedValues.ContainsKey(segment))
                {
                    updatedValues.Add(segment, 1f);// observedParticles.particles.Length);
                }
                else
                {
                    updatedValues[segment]++;//= 1f / observedParticles.particles.Length;
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
                return (values[segment] / observedParticles.particles.Length) / (range/resolution);
            }
            else
            {
                return 0;
            }
        }
    }
}

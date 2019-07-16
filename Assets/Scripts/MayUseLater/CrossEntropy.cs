using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrossEntropy : MonoBehaviour, IPlottable
{
    [SerializeField]
    Distribution q;
    [SerializeField]
    ParticleManager p;
    [SerializeField]
    float lowerBound;
    [SerializeField]
    float upperBound;
    [SerializeField]
    int resolution;
    [SerializeField]
    float ce;
    [HideInInspector]
    public float lowEntropyRange;
    public event UnityAction lowCrossEntropy;


    public float CE
    {
        get
        {
            return ce;
        }
        set
        {
            if (value <= lowEntropyRange)
            {
                lowCrossEntropy?.Invoke();
            }
            ce= value;
        }
    }

    float LowerBound
    {
        get
        {
            return lowerBound;
        }
        set
        {
            lowerBound = value;
        }
    }

    float UpperBound
    {
        get
        {
            return upperBound;
        }
        set
        {
            upperBound = value;
        }
    }

    float Range
    {
        get
        {
            return UpperBound - LowerBound;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float c = 0;
        foreach(Particle p in p.particles)
        {
            c += Mathf.Log10(q.Value(p.X));
        }
        CE = -c / p.particles.Length;
    }

    public Dictionary<string, float> Data()
    {
        Dictionary<string, float> data = new Dictionary<string, float>();
        data.Add("CrossEntropy", CE);
        return data;
    }
}

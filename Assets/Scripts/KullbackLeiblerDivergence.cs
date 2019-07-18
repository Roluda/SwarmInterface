using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KullbackLeiblerDivergence : MonoBehaviour, IPlottable
{
    [SerializeField]
    Distribution p;
    [SerializeField]
    Distribution q;
    [SerializeField]
    float lowerBound;
    [SerializeField]
    float upperBound;
    [SerializeField]
    int resolution;
    [SerializeField]
    float divergence;
    [SerializeField]
    public float lowDivergenceRange;
    public event UnityAction lowDivergence;


    public float Divergence
    {
        get
        {
            return divergence;
        }
        set
        {
            if (value <= lowDivergenceRange)
            {
                lowDivergence?.Invoke();
            }
            divergence = value;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LowerBound =  Mathf.Min(p.LowerBound, q.LowerBound);
        UpperBound =  Mathf.Max(p.UpperBound, q.UpperBound);
        float div = 0;
        for(int i=0; i < resolution; i++)
        {
            float x = LowerBound + i * (Range/resolution);
            float val = p.Value(x) * Mathf.Log10(p.Value(x) / q.Value(x));
            if (float.IsNaN(val) )
            {
                val = 0;
            }else if(float.IsInfinity(val))
            {
                val = 38;
            }            
            div += val;
        }
        Divergence = div;
    }

    public Dictionary<string, float> Data()
    {
        Dictionary<string, float> data = new Dictionary<string, float>();
        data.Add("KL", Divergence);
        return data;
    }
}

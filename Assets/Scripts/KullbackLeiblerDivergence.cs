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
    int resolution; //continous space has to be discretized, this determines how many discrete points there is for approximation
    [SerializeField]
    float divergence;
    [SerializeField]
    public float lowDivergenceRange; //threshhold to trigger lowDivergence event
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
        LowerBound =  Mathf.Min(p.LowerBound, q.LowerBound); //gets the lowest lowerBound
        UpperBound =  Mathf.Max(p.UpperBound, q.UpperBound); //gets the highest upperBound
        float div = 0;
        for(int i=0; i < resolution; i++)
        {
            float x = LowerBound + i * (Range/resolution);
            float val = p.Value(x) * Mathf.Log10(p.Value(x) / q.Value(x));
            if (float.IsNaN(val) )
            {
                val = 0;
            }else if(float.IsInfinity(val))//needs to check this since the histogram may has empty segments & being far away from target distribution may return super low floats.
            {
                val = 38; //1*e^38 is lowest possible float.
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

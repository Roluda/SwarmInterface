using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour, IPlottable
{
    public Particle[] particles;
    public NormalDistribution distribution = new NormalDistribution(0, 1);


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
            float sum = 0;
            foreach(Particle p in particles)
            {
                sum += p.X;
            }
            return sum / particles.Length;
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
        Setup(initialParticles, initialMean, initialStandardDeviation);
    }

    public void Update()
    {
        foreach(Particle p in particles)
        {
            ApplyDrift(p);
        }
    }

    public void LateUpdate()
    {
        distribution.Mean = PositionAverage;
        distribution.StandardDeviation = PositionStandardDeviation;
    }

    #endregion

    public void Setup(int particleCount, float mean, float standardDeviation)
    {
        particles = new Particle[particleCount];
        for (int i = 0; i < particleCount; i++)
        {
            float normalDistributedRandomNumber = MarsagliaGenerator.Next() * standardDeviation + mean;
            particles[i] = new Particle(normalDistributedRandomNumber);
        }
        distribution = new NormalDistribution(PositionAverage, PositionStandardDeviation);
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

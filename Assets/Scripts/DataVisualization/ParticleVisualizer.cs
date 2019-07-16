using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleVisualizer : Visualizer
{
    [SerializeField]
    ParticleSystem theParticleSystem;

    ParticleSystem.Particle[] particles;

    public override void Visualize()
    {
        int particleCount = theParticleSystem.GetParticles(particles);
        for(int i = 0; i < particleCount; i++)
        {
            float x = Center+(particles[i].position.x / Width) * HorizontalScale;
            float y = ObservedDistribution.Value(x) / VerticalScale * Height - Height / 2;
            if (particles[i].position.y > y)
            {
                particles[i].remainingLifetime = 0;
            }
        }
        theParticleSystem.SetParticles(particles, particleCount);
    }

    protected override void Setup()
    {
        transform.position = Vector3.zero;
        particles = new ParticleSystem.Particle[theParticleSystem.main.maxParticles];
        Vector3 scale = new Vector3(Width, Height, 1);
        ParticleSystem.ShapeModule shapeModule = theParticleSystem.shape;
        shapeModule.scale = scale;
    }
}

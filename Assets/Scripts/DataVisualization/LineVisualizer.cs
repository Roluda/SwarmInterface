﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineVisualizer : Visualizer
{
    [SerializeField]
    int resolution;
    [SerializeField]
    LineRenderer line;

    /// <summary>
    /// puts line renderer points from left to right according to coordinate space and observed distribution
    /// </summary>
    public override void Visualize()
    {
        Vector3[] curvePoints = new Vector3[resolution];
        float interval = Width/resolution;
        for (int i = 0; i < resolution; i++)
        {
            float x = -Width / 2 + interval * i;
            float y = ObservedDistribution.Value(Center+(x/Width)*HorizontalScale)/VerticalScale*Height - Height/2;
            curvePoints[i] = new Vector3(x, y, 0);
        }
        line.positionCount = resolution;
        line.SetPositions(curvePoints);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{

    [SerializeField]
    Camera displayCamera;
    [SerializeField]
    protected Distribution observedDistribution;
    [SerializeField]
    CoordinateSpace coordinateSpace;
    [SerializeField]
    bool alwaysUpdate;
    public Distribution ObservedDistribution
    {
        get
        {
            return observedDistribution;
        }
        set
        {            
            observedDistribution = value;
        }
    }

    public float Center
    {
        get
        {
            return coordinateSpace.Center;
        }
    }

    public float HorizontalScale{
        get
        {
            return coordinateSpace.HorizontalScale;
        }
    }

    public float VerticalScale
    {
        get
        {
            return coordinateSpace.VerticalScale;
        }
    }

    public float Width
    {
        get
        {
            return displayCamera.ScreenToWorldPoint(new Vector3(displayCamera.pixelWidth, 0, 0)).x - displayCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        }
    }
    public float Height
    {
        get
        {
            return displayCamera.ScreenToWorldPoint(new Vector3(0, displayCamera.pixelHeight, 0)).y - displayCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        }
    }


    protected bool dirty;

    public void Start()
    {
        observedDistribution.ValueChanged += () => dirty = true;
        coordinateSpace.ScaleChanged += () => dirty = true;
        Setup();
    }

    public void LateUpdate()
    {
        if (dirty)
        {
            if (!alwaysUpdate)
            {
                dirty = false;
            }
            Visualize();
        }
    }

    virtual protected void Setup()
    {

    }

    virtual public void Visualize()
    {

    }
}

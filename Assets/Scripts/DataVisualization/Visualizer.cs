using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{

    [SerializeField]
    Camera displayCamera;
    [SerializeField]
    float horizontalScale;
    [SerializeField]
    float verticalScale;
    [SerializeField]
    float center;

    protected NormalDistribution observedDistribution;
    public NormalDistribution ObservedDistribution
    {
        get
        {
            return observedDistribution;
        }
        set
        {
            value.OnDistributionChange += () => dirty = true;
            observedDistribution = value;
        }
    }

    public float HorizontalScale{
        get
        {
            return horizontalScale;
        }
        set
        {
            if (horizontalScale != value)
            {
                horizontalScale = value;
            }
            dirty = true;
        }
    }

    public float VerticalScale
    {
        get
        {
            return verticalScale;
        }
        set
        {
            if (verticalScale != value)
            {
                verticalScale = value;
            }
            dirty = true;
        }
    }

    public float Center
    {
        get
        {
            return center;
        }
        set
        {
            if (center != value)
            {
                center = value;
            }
            dirty = true;
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
        Setup();
    }
    public void Update()
    {
        dirty = false;
    }

    public void LateUpdate()
    {
        if (dirty)
        {
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

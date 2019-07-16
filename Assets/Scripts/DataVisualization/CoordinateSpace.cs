using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoordinateSpace : MonoBehaviour
{
    [SerializeField]
    List<Distribution> distributionsInSpace;
    [SerializeField]
    float verticalScale;
    [SerializeField]
    float horizontalScale;
    [SerializeField]
    float center;
    [SerializeField]
    bool autoSetVertical;
    [SerializeField]
    float verticalMargin;
    [SerializeField]
    bool autoSetHorizontal;
    [SerializeField]
    bool autoSetCenter;
    [SerializeField]
    float smoothTime;
    [SerializeField]
    float smoothSpeed;
    public event UnityAction ScaleChanged;

    float vSpeed;
    float hSpeed;
    float cSpeed;

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
                verticalScale = Mathf.SmoothDamp(verticalScale, value, ref vSpeed, smoothTime);
                ScaleChanged?.Invoke();
            }
        }
    }

    public float HorizontalScale
    {
        get
        {
            return horizontalScale;
        }
        set
        {
            if (horizontalScale != value)
            {
                horizontalScale = Mathf.SmoothDamp(horizontalScale,value, ref hSpeed, smoothTime);
                ScaleChanged?.Invoke();
            }
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
                center = Mathf.SmoothDamp(center, value, ref cSpeed, smoothTime);
                ScaleChanged?.Invoke();
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float[] maxima = new float[distributionsInSpace.Count];
        float[] lowerBounds = new float[distributionsInSpace.Count];
        float[] upperBounds = new float[distributionsInSpace.Count];
        for(int i=0; i < distributionsInSpace.Count; i++)
        {
            maxima[i] = distributionsInSpace[i].Maximum;
            lowerBounds[i] = distributionsInSpace[i].LowerBound;
            upperBounds[i] = distributionsInSpace[i].UpperBound;
        }
        if (autoSetVertical)
        {
            float scale = Mathf.Max(maxima);
            VerticalScale = scale * (1 + verticalMargin / 100);
        }
        if (autoSetHorizontal)
        {
            float scale = Mathf.Max(upperBounds) - Mathf.Min(lowerBounds);
            HorizontalScale = scale;
        }
        if (autoSetCenter)
        {
            float center = (Mathf.Max(upperBounds) + Mathf.Min(lowerBounds)) / 2;
            Center = center;
        }
    }
}

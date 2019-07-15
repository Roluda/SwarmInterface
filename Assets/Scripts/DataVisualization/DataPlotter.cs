using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Reflection;

public class DataPlotter : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    LineRenderer render;
    [SerializeField]
    float plotInterval;
    [SerializeField]
    int resolution;
    [SerializeField]
    PlottableObserver observedData;
    [SerializeField]
    string dataKey;

    float time;

    List<Vector2> dataPoints = new List<Vector2>(1);

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > plotInterval)
        {
            time = 0;
            Plot();
        }
    }

    void Plot()
    {
        Vector2 newPoint = new Vector2(rectTransform.rect.width, observedData.GetData(dataKey)/rectTransform.rect.height);
        float min = newPoint.y;
        foreach(Vector2 v in dataPoints)
        {
            if (v.y < min) min = v.y;
        }
        float max = newPoint.y;
        foreach(Vector2 v in dataPoints)
        {
            if (v.y > max) max = v.y;
        }
        float heightSpan = max - min;
        if (dataPoints.Count >= resolution)
        {
            dataPoints.Remove(dataPoints[0]);
        }
        dataPoints.Add(newPoint);
        Vector3[] scaledPoints = new Vector3[dataPoints.Count];
        for (int i= 0; i < dataPoints.Count; i++)
        {
            float x = dataPoints[i].x - i / rectTransform.rect.width;
            float y = dataPoints[i].y / heightSpan;
            scaledPoints[i] = new Vector3(x, y,0);
        }
        render.positionCount = resolution;
        render.SetPositions(scaledPoints);
    }
}

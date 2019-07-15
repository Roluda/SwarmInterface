using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlottableObserver : MonoBehaviour
{
    [SerializeField]
    string[] availiableVariables;

    IPlottable obs;
    void Awake()
    {
        obs = GetComponent<IPlottable>();
        availiableVariables = new string[obs.Data().Count];
        int i = 0;
        foreach(KeyValuePair<string,float> kvp in obs.Data())
        {
            availiableVariables[i] = kvp.Key;
            i++;
        }
    }

    public float GetData(string key)
    {
        foreach(KeyValuePair<string,float> kvp in obs.Data())
        {
            if (kvp.Key == key)
            {
                return kvp.Value;
            }
        }
        Debug.LogError("There is no data for key " + key);
        return 0;
    }
}

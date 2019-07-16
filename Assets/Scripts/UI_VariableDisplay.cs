using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_VariableDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text variableName;
    [SerializeField]
    TMP_Text variableAmount;
    [SerializeField]
    int numDecimalsRound;
    [SerializeField]
    PlottableObserver observer;
    [SerializeField]
    string variableToCheck;

    // Start is called before the first frame update
    void Start()
    {
        variableName.text = variableToCheck + ":";
    }

    // Update is called once per frame
    void Update()
    {
        variableAmount.text = System.Math.Round(observer.GetData(variableToCheck), numDecimalsRound).ToString();
    }
}

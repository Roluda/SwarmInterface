using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlottable
{
    Dictionary<string, float> Data();
}

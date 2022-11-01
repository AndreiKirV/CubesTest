using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _value;

    public float Value => _value;

    public void Update() 
    {
        _value += Time.deltaTime;
    }
}
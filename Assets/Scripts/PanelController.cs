using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _spawnTime;
    [SerializeField] private TMP_InputField _speed;
    [SerializeField] private TMP_InputField _distance;
    
    public static UnityEvent <float, float, float> ValuesIsSet = new UnityEvent<float, float, float>();

    private void Awake() 
    {
        _spawnTime.onValueChanged.AddListener(GiveValues);
        _speed.onValueChanged.AddListener(GiveValues);
        _distance.onValueChanged.AddListener(GiveValues);
    }
    
    public void GiveValues(string targetString)
    {
        float spawnTime;
        float speed;
        float distance;

        if(float.TryParse(_spawnTime.text, out spawnTime) && float.TryParse(_speed.text, out speed) && float.TryParse(_distance.text, out distance) && ValuesIsSet != null)
        {
            ValuesIsSet.Invoke(spawnTime, speed, distance);
        }
    }
}
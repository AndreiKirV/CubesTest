using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private Vector2Int _minPosition;
    [SerializeField] private Vector2Int _maxPosition;
    private float _minY = -6;
    private float _spawnStep;
    private float _targetSpeed;
    private float _targetDistance;
    private float _previousSpawn;
    private Timer _timer;
    private Dictionary<GameObject, Cube> _cubes = new Dictionary<GameObject, Cube>();

    private void Start() 
    {
        _timer = new Timer();
        _previousSpawn = _timer.Value;
        PanelController.ValuesIsSet.AddListener(SetValues);
    }

    private void Update() 
    {
        _timer.Update();

        if (_previousSpawn + _spawnStep <= _timer.Value && _spawnStep > 0 && _targetSpeed > 0 && _targetDistance > 0)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject tempObject = Instantiate<GameObject>(_targetObject, new Vector3(Random.Range(_minPosition.x, _maxPosition.x), _minY, Random.Range(_minPosition.y, _maxPosition.y)), Quaternion.identity);
        tempObject.transform.rotation = Quaternion.Euler(tempObject.transform.rotation.x,Random.Range(0,360), transform.rotation.eulerAngles.z);
        Cube tempCube = tempObject.AddComponent<Cube>();
        tempCube.SetSpeed(_targetSpeed);
        tempCube.SetMaxDistance(_targetDistance);
        tempCube.Death.AddListener(DeleteCube);
        tempCube.SetMaterial(_targetMaterial);
        _cubes.Add(tempObject, tempCube);
        _previousSpawn = _timer.Value;
    }

    private void DeleteCube(GameObject targetObject)
    {
        _cubes.Remove(targetObject);
    }

    private void SetValues(float spawnStep, float cubeSpeed, float cubeDistance)
    {
        _spawnStep = spawnStep;
        _targetSpeed = cubeSpeed;
        _targetDistance = cubeDistance;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    private float _speed;
    private float _distance;
    private float _speedOfChangeInTransparency = 1.5f;
    private Vector3 _startPosition;
    private MeshRenderer _renderer;

    public UnityEvent <GameObject> Death = new UnityEvent<GameObject>();

    private void Start() 
    {
        _startPosition = transform.position;
        _renderer.GetComponent<MeshRenderer>();
    }

    private void Update() 
    {
        if (Vector3.Distance(_startPosition, transform.position) <= _distance)
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + _speed, transform.position.z), Time.deltaTime);
        else
        {
            if (Death != null)
            Death.Invoke(gameObject);
            Destroy(gameObject);
        }
        
        if (_renderer.material.color.a < 1)
            _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, _renderer.material.color.a + (_speedOfChangeInTransparency * Time.deltaTime));
        else if (_renderer.material.color.a >= 1)
            _renderer.material.color = new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f), 0.5f);

    }

    public void SetMaterial(Material material)
    {
        _renderer = gameObject.GetComponent<MeshRenderer>();
        _renderer.material = material;
        _renderer.material.color = new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f), 0f);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetMaxDistance(float distance)
    {
        _distance = distance;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoving : MonoBehaviour
{
    [SerializeField] float _horizontalRange, _verticalRange, _frequency;
    Transform _transform;
    Vector3 _movement, _startPosition;
    private void Start()
    {
        _transform = transform;
        _movement = Vector3.zero;
        _startPosition = transform.position;
    }
    private void Update()
    {
        var sin = Mathf.Sin(Time.time * _frequency);
        _movement.x = sin * _horizontalRange;
        _movement.y = sin * _verticalRange;
        _transform.position = _startPosition + _movement;
    }
}

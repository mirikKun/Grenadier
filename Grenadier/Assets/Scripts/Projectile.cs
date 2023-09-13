using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeLenght = 5;
    [SerializeField] private float collisionDetectionLenght = 0.3f;
    private Vector3 _startSpeed;
    private Vector3 _curSpeed;
    private Vector3 _startPoint;
    private Transform _transform;
    private float _time;
    private const float g = 9.81f;
    private const int ObstacleLayerMask = 1 << 9;
    [SerializeField] private int reflectionsMaxCount = 5;
    private int _reflectionsCurCount = 0;


    public void SetupProjectile(Vector3 startSpeed, Vector3 startPoint)
    {
        _reflectionsCurCount = 0;
        _transform = transform;
        _startSpeed = startSpeed;
        _curSpeed = startSpeed;
        _startPoint = startPoint;
        _transform.position = startPoint;
        Destroy(gameObject, lifeLenght);
    }

    private void Update()
    {
        _time += Time.deltaTime;
        UpdatePosition();
        if (_reflectionsCurCount < reflectionsMaxCount)
        {
            CheckOnCollision();
        }
    }

    private void UpdatePosition()
    {
        Vector3 newPoint = _startPoint + _startSpeed * _time;
        newPoint.y = _startPoint.y + _startSpeed.y * _time - g * _time * _time / 2;
        _transform.position = newPoint;
    }

    private void CheckOnCollision()
    {
        _curSpeed.y = _startSpeed.y - g * _time;
        if (Physics.Raycast(transform.position, _curSpeed, out var hit, collisionDetectionLenght, ObstacleLayerMask))
        {
            Vector3 newSpeed = Vector3.Reflect(_curSpeed, hit.normal);
            Reflect(newSpeed);
            _reflectionsCurCount++;
        }
    }
    
    private void Reflect(Vector3 newSpeed)
    {
        _time = 0;
        _startSpeed = newSpeed;
        _curSpeed = newSpeed;
        _startPoint = _transform.position;
    }
}
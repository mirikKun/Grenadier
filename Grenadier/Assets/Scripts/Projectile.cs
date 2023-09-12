using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeLenght=5;
    private Vector3 _startSpeed;
    private Vector3 _startPoint;
    private Transform _transform;
    private float _time;
    private const float g = 9.81f;

    public void SetupProjectile(Vector3 startSpeed, Vector3 startPoint)
    {
        _time = 0;
        _transform = transform;
        _startSpeed = startSpeed;
        _startPoint = startPoint;
        _transform.position = startPoint;
        Destroy(gameObject,lifeLenght);

    }

    private void Update()
    {
        _time += Time.deltaTime;
        Vector3 newPoint = _startPoint + _startSpeed * _time;
        newPoint.y = _startPoint.y + _startSpeed.y * _time - g * _time * _time / 2;
        _transform.position = newPoint;
    }
}
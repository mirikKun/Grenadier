using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeLenght = 5;
    [SerializeField] private float collisionDetectionLenght = 0.3f;
    private int _reflectionsMaxCount = 5;
    private ExplosionsPool _pool;
    private Vector3 _startSpeed;
    private Vector3 _curSpeed;
    private Vector3 _startPoint;
    private Transform _transform;
    private float _flyTime;
    private float _totalTime;
    private const float g = 9.81f;
    private const int ObstacleLayerMask = 1 << 9;
    private int _reflectionsCurCount;
    public event Action<Projectile> OnProjectileEnd;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetExplosionsPool(ExplosionsPool pool)
    {
        _pool = pool;
    }

    public void SetupProjectile(Vector3 startSpeed, Vector3 startPoint, int reflectionsCount)
    {
        _flyTime = 0;
        _totalTime = 0;
        _reflectionsMaxCount = reflectionsCount;
        _reflectionsCurCount = 0;
        _startSpeed = startSpeed;
        _curSpeed = startSpeed;
        _startPoint = startPoint;
        _transform.position = startPoint;
    }

    public bool GameUpdate()
    {
        _flyTime += Time.deltaTime;
        _totalTime += Time.deltaTime;
        UpdatePosition();
        if (!CheckOnCollision() || !CheckOnLifeTime())
        {
            return false;
        }
        return true;

    }

    private void UpdatePosition()
    {
        Vector3 newPoint = _startPoint + _startSpeed * _flyTime;
        newPoint.y = _startPoint.y + _startSpeed.y * _flyTime - g * _flyTime * _flyTime / 2;
        _transform.position = newPoint;
    }

    private bool CheckOnCollision()
    {
        _curSpeed.y = _startSpeed.y - g * _flyTime;
        if (Physics.Raycast(transform.position, _curSpeed, out var hit, collisionDetectionLenght, ObstacleLayerMask))
        {
            if (_reflectionsCurCount < _reflectionsMaxCount)
            {
                float bounciness = hit.collider.material.bounciness;

                Vector3 newSpeed = Vector3.Reflect(_curSpeed, hit.normal)*bounciness;
                Reflect(newSpeed);
                _reflectionsCurCount++;
            }
            else
            {
                Debug.Log("ssssss");
                BlowUp(hit);
                return false;

            }
        }
        return true;

    }

    private bool CheckOnLifeTime()
    {
        if (_totalTime > lifeLenght)
        {
            BlowUp();
            return false;

        }

        return true;
    }

    private void BlowUp(RaycastHit hit)
    {
        Explosion explosion = _pool.GetElement();
        explosion.PlaceExplosion(hit.point,hit.normal);
        _pool.GetMarkCreator.CreateMark(hit.point,hit.normal);
        OnProjectileEnd?.Invoke(this);
    }
    private void BlowUp()
    {
        Explosion explosion = _pool.GetElement();
        explosion.PlaceExplosion(_transform.position,Vector3.up);
        OnProjectileEnd?.Invoke(this);

    }

    private void Reflect(Vector3 newSpeed)
    {
        _flyTime = 0;
        _startSpeed = newSpeed;
        _curSpeed = newSpeed;
        _startPoint = _transform.position;
    }
}
using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int lifeTime;
    [SerializeField] private ParticleSystem[] particleSystems;
    private float _progress;
    private Transform _transform;
    public event Action<Explosion> OnExplosionEnded;

    private void Awake()
    {
        _transform = transform;
    }

    public void PlaceExplosion(Vector3 position, Vector3 normal)
    {
        _transform.position = position;
        _transform.up = normal;
        _progress = 0;
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }

    public bool GameUpdate()
    {
        _progress += Time.deltaTime;
        if (_progress >= lifeTime)
        {
            OnExplosionEnded?.Invoke(this);
            return false;
        }

        return true;
    }
}
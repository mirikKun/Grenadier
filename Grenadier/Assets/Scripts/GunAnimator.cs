using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    [Header("Shaking")]
    [SerializeField] private Transform camera;
    [SerializeField] private float shakingAmplitude;
    [SerializeField] private float shakingSpeed;
    private float _shakingProgress;
    
    [Header("Recoil")] 
    [SerializeField] private Transform muzzle;
    [SerializeField] private float recoilAmplitude;
    [SerializeField] private float recoilSpeed;
    private Vector3 _startMuzzlePosition;
    private float _muzzleOffset;
    private  Vector3 _direction =Vector3.up;
    private float _recoilProgress;

    public void StopAnimations()
    {
        StopAllCoroutines();
    }
    public void StartCameraShaking()
    {
        StartCoroutine(CameraShaking());
    }

    public void StartRecoilAnimation()
    {
        StartCoroutine(MuzzleRecoil());
    }

    private IEnumerator CameraShaking()
    {
        _shakingProgress = 0;
        Vector3 initialPos = camera.localPosition;
        Vector3 newPosition = initialPos + Random.insideUnitSphere * shakingAmplitude;
        camera.localPosition = newPosition;

        Vector3 backDirection = (initialPos - newPosition).normalized;
        float distance = (initialPos - newPosition).magnitude;

        while (_shakingProgress < 1)
        {
            float offset = Time.time * shakingSpeed;
            camera.localPosition += backDirection * offset;
            _shakingProgress += offset / distance;
            yield return null;
        }

        camera.localPosition = initialPos;
    }

    private IEnumerator MuzzleRecoil()
    {
        _startMuzzlePosition = muzzle.localPosition;
        _recoilProgress = 0;
        _muzzleOffset = 0;
        while (_recoilProgress < 1)
        {
            
            muzzle.localPosition = _startMuzzlePosition + _direction * _muzzleOffset;
            float offset = Time.time * recoilSpeed;
            _recoilProgress += offset / (recoilAmplitude*2);

            if (_recoilProgress < 0.5f)
            {
                _muzzleOffset += offset;
            }
            else
            {
                _muzzleOffset -= offset;
            }

            yield return null;
        }

        muzzle.localPosition = _startMuzzlePosition;
    }
}
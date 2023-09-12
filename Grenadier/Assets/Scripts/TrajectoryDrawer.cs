using System;
using UnityEditor;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [SerializeField] private Transform launchPointTransform;
    [SerializeField] private Transform muzzle;
    [SerializeField] private int pointCount = 30;
    [SerializeField] private float timeBetween = 0.3f;
    [SerializeField] private LineRenderer lineRenderer;
    private const float g = 9.81f;


    public void Draw(float power)
    {
        Vector3 launchPoint = launchPointTransform.position;
        Vector3 dir = -muzzle.up;
        Vector3 startSpeed = dir * power;
        lineRenderer.positionCount = pointCount;
        for (int i = 0; i < pointCount; i++)
        {
            float timeStep = i * timeBetween;
            Vector3 newPoint = launchPoint + startSpeed * timeStep;
            newPoint.y =launchPoint.y+ startSpeed.y * timeStep - g * timeStep * timeStep / 2;
            lineRenderer.SetPosition(i,newPoint);
        }
    }
}
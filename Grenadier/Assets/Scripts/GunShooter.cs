
    using System;
    using UnityEngine;

    public class GunShooter:MonoBehaviour
    {

        [SerializeField] private ProjectileGenerator projectileGenerator;
        [SerializeField] private TrajectoryDrawer trajectoryDrawer;
        [SerializeField] private Transform launchPoint;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float power;
        private void Update()
        {
            trajectoryDrawer.Draw(power);
            if (Input.GetButtonDown("Fire1"))
            {
                Projectile newProjectile = projectileGenerator.GenerateProjectile();
                newProjectile.SetupProjectile(-muzzle.up*power,launchPoint.position);
            }
        }
    }

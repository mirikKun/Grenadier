using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private UISettings uiSettings;
    [SerializeField] private TrajectoryDrawer trajectoryDrawer;
    [SerializeField] private GunAnimator gunAnimator;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float power = 20;
    [SerializeField] private int bouncesCount = 2;


    public void StartBehaviour()
    {
        uiSettings.SetupStartValues(power, bouncesCount);
    }

    public void GameUpdate()
    {           

        trajectoryDrawer.Draw(power);
        if (Input.GetButtonDown("Fire1"))
        {
            Projectile newProjectile = projectilePool.GetElement();
            newProjectile.SetupProjectile(-muzzle.up * power, launchPoint.position, bouncesCount);
            gunAnimator.StopAnimations();
            gunAnimator.StartRecoilAnimation();
            gunAnimator.StartCameraShaking();
        }
    }

    private void OnEnable()
    {
        uiSettings.PowerSlider.onValueChanged.AddListener(SetPower);
        uiSettings.BouncesCountField.onValueChanged.AddListener(SetBouncesCount);
    }

    private void OnDisable()
    {
        uiSettings.PowerSlider.onValueChanged.RemoveListener(SetPower);
        uiSettings.BouncesCountField.onValueChanged.RemoveListener(SetBouncesCount);
    }

    private void SetPower(float newPower)
    {
        power = newPower;
    }

    private void SetBouncesCount(string count)
    {
        bouncesCount = int.Parse(count);
    }
}
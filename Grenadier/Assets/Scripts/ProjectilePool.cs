
using UnityEngine;

[RequireComponent(typeof(ProjectileGenerator))]
public class ProjectilePool : SimpleObjectPool<Projectile>
{
    private ProjectileGenerator _projectileGenerator;
    [SerializeField] private ExplosionsPool explosionPool;
    private void Awake()
    {
        _projectileGenerator = GetComponent<ProjectileGenerator>();
    }

    protected override void GenerateNewElement()
    {
        Projectile newElement = _projectileGenerator.GenerateProjectile();
        newElement.transform.SetParent(transform);
        newElement.gameObject.SetActive(false);
        newElement.SetExplosionsPool(explosionPool);
        newElement.OnProjectileEnd += RevertToPool;
        _elements.Add(newElement);

    }
}

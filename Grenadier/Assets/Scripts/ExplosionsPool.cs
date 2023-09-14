using System;
using UnityEngine;

public class ExplosionsPool : SimpleObjectPool<Explosion>
{
    [SerializeField] private Explosion prefab;
    [SerializeField] private ExplosionMarkCreator explosionMarkCreator;
    public ExplosionMarkCreator GetMarkCreator => explosionMarkCreator;
    protected override void GenerateNewElement()
    {
        if (prefab == null)
        {
            Debug.LogError("Need a reference to the destination prefab");
        }

        Explosion newElements = Instantiate(prefab, transform);
        newElements.gameObject.SetActive(false);
        newElements.OnExplosionEnded += RevertToPool;
        _elements.Add(newElements);

    }

    private void OnDestroy()
    {
        foreach (var element in _elements)
        {
            element.OnExplosionEnded -= RevertToPool;
        }
    }
}
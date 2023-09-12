using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{
    [SerializeField] private float cubeSize = 1f;
    [SerializeField] private float randomMaxOffset=0.2f;
    [SerializeField] private Material cubeMaterial;


    private float GetRandomOffset()
    {
        return cubeSize / 2 + Random.Range(-randomMaxOffset, randomMaxOffset);
    }

    public Projectile GenerateProjectile()
    {
        GameObject projectileObject = new GameObject("Projectile");

        MeshRenderer meshRenderer = projectileObject.AddComponent<MeshRenderer>();
        meshRenderer.material = cubeMaterial;
        
        MeshFilter meshFilter = projectileObject.AddComponent<MeshFilter>();
        Mesh projectileMesh = new Mesh();

        Vector3[] vertices =
        {
            new Vector3(-GetRandomOffset(), -GetRandomOffset(), -GetRandomOffset()),
            new Vector3(GetRandomOffset(), -GetRandomOffset(), -GetRandomOffset()),
            new Vector3(GetRandomOffset(), GetRandomOffset(), -GetRandomOffset()),
            new Vector3(-GetRandomOffset(), GetRandomOffset(), -GetRandomOffset()),
            new Vector3(-GetRandomOffset(), GetRandomOffset(), GetRandomOffset()),
            new Vector3(GetRandomOffset(), GetRandomOffset(), GetRandomOffset()),
            new Vector3(GetRandomOffset(), -GetRandomOffset(), GetRandomOffset()),
            new Vector3(-GetRandomOffset(),-GetRandomOffset(), GetRandomOffset()),
        };

        int[] triangles =
        {
            0, 2, 1, 
            0, 3, 2,
            2, 3, 4, 
            2, 4, 5,
            1, 2, 5, 
            1, 5, 6,
            0, 7, 4, 
            0, 4, 3,
            5, 4, 7,
            5, 7, 6,
            0, 6, 7, 
            0, 1, 6
        };

        projectileMesh.vertices = vertices;
        projectileMesh.triangles = triangles;

        projectileMesh.RecalculateNormals();

        meshFilter.mesh = projectileMesh;
        Projectile newProjectile = projectileObject.AddComponent<Projectile>();
        Debug.Log("bbb");

        return newProjectile;
    }
}
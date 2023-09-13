using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{
    [SerializeField] private float cubeSize = 1f;
    [SerializeField] private float randomMaxOffset = 0.2f;
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


        Vector3[] vertices = GetVertices();
        int[] triangles = GetTriangles();

        projectileMesh.vertices = vertices;
        projectileMesh.triangles = triangles;

        projectileMesh.Optimize();
        projectileMesh.RecalculateNormals();
        meshFilter.mesh = projectileMesh;

        Projectile newProjectile = projectileObject.AddComponent<Projectile>();


        return newProjectile;
    }

    private Vector3[] GetRandomVertices()
    {
        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-GetRandomOffset(), -GetRandomOffset(), -GetRandomOffset());
        vertices[1] = new Vector3(GetRandomOffset(), -GetRandomOffset(), -GetRandomOffset());
        vertices[2] = new Vector3(GetRandomOffset(), -GetRandomOffset(), GetRandomOffset());
        vertices[3] = new Vector3(-GetRandomOffset(), -GetRandomOffset(), GetRandomOffset());
        vertices[4] = new Vector3(-GetRandomOffset(), GetRandomOffset(), -GetRandomOffset());
        vertices[5] = new Vector3(GetRandomOffset(), GetRandomOffset(), -GetRandomOffset());
        vertices[6] = new Vector3(GetRandomOffset(), GetRandomOffset(), GetRandomOffset());
        vertices[7] = new Vector3(-GetRandomOffset(), GetRandomOffset(), GetRandomOffset());
        return vertices;
    }

    private Vector3[] GetVertices()
    {
        Vector3[] vertices = new Vector3[6 * 4];

        Vector3[] randomVertices=GetRandomVertices();
        
        // Down face
        vertices[0] = randomVertices[0];
        vertices[1] = randomVertices[1];
        vertices[2] = randomVertices[2];
        vertices[3] = randomVertices[3];

        // Top face
        vertices[4] = randomVertices[4];
        vertices[5] = randomVertices[7];
        vertices[6] = randomVertices[6];
        vertices[7] = randomVertices[5];

        // Front face
        vertices[8] = randomVertices[0];
        vertices[9] = randomVertices[4];
        vertices[10] = randomVertices[5];
        vertices[11] = randomVertices[1];

        // Back face
        vertices[12] = randomVertices[3];
        vertices[13] = randomVertices[2];
        vertices[14] = randomVertices[6];
        vertices[15] = randomVertices[7];

        // Left face
        vertices[16] = randomVertices[0];
        vertices[17] = randomVertices[3];
        vertices[18] = randomVertices[7];
        vertices[19] = randomVertices[4];

        // Right face
        vertices[20] = randomVertices[1];
        vertices[21] = randomVertices[5];
        vertices[22] = randomVertices[6];
        vertices[23] = randomVertices[2];
        return vertices;
    }

    private int[] GetTriangles()
    {
        int[] triangles = new int[6 * 2 * 3];
        for (int i = 0, j = 0; i < 6; i++)
        {
            triangles[j] = i * 4;
            triangles[j + 1] = i * 4 + 1;
            triangles[j + 2] = i * 4 + 2;
            triangles[j + 3] = i * 4;
            triangles[j + 4] = i * 4 + 2;
            triangles[j + 5] = i * 4 + 3;
            j += 6;
        }

        return triangles;
    }
}
using UnityEngine;

public class ExplosionMarkCreator : MonoBehaviour
{
    [SerializeField] private Transform mark;

    public void CreateMark(Vector3 position, Vector3 normal)
    {
        var newMark = Instantiate(mark, position + normal * 0.01f, Quaternion.identity);
        newMark.up = normal;
    }
}
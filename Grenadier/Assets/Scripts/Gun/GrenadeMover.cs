using UnityEngine;

public class GrenadeMover : MonoBehaviour
{
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [SerializeField] private Vector2 xRotationEdges;
    [SerializeField] private Vector2 yRotationEdges;
    [SerializeField] private bool inverseMouse;
    [SerializeField] private Transform grenadeTransform;
    [SerializeField] private Transform playerTransform;
    private int YDir => inverseMouse ? 1 : -1;
    private float _xRotation;
    private float _yRotation;

    public void GameUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityY;

        _xRotation += YDir * mouseY;
        _xRotation = Mathf.Clamp(_xRotation, yRotationEdges.x, yRotationEdges.y);
        _yRotation += mouseX;
        _yRotation = Mathf.Clamp(_yRotation, xRotationEdges.x, xRotationEdges.y);

        playerTransform.rotation = Quaternion.Euler(0, _yRotation, 0);
        grenadeTransform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}
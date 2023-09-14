using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Texture2D stampTexture;

    [SerializeField] private Vector2Int position = new Vector2Int(1024, 1024);
    [SerializeField] private Color backColor = Color.white;
    private List<Vector2Int> _points = new List<Vector2Int>();
    private Texture2D _texture;
    private int GetStampHalfSize => stampTexture.width / 2;

    private void Start()
    {
        _texture = new Texture2D(renderTexture.width, renderTexture.height);
        renderer.material.mainTexture = _texture;
        ResetTexture();

        RenderTexture.active = renderTexture;

        GL.Clear(true, true, backColor);
        ResetTexture();
        RenderTexture.active = null;
    }

    private void ResetTexture()
    {
        Color[] pixels = new Color[renderTexture.width * renderTexture.height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = backColor;
        }

        _texture.SetPixels(pixels);
        _texture.Apply();
    }

    public void AddPoint(Vector2 vector2)
    {
        Vector2Int intVector =
            new Vector2Int((int)(vector2.x * renderTexture.width), (int)(vector2.y * renderTexture.width));
        _points.Add(intVector);
        Rerender();
    }

    private void Rerender()
    {
        RenderTexture.active = renderTexture;

        _texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0, false);
        foreach (var point in _points)
        {
            int xLeft = Mathf.Clamp(point.x - GetStampHalfSize, 0, renderTexture.width);
            int xRight = Mathf.Clamp(point.x + GetStampHalfSize, 0, renderTexture.width);
            int yDown = Mathf.Clamp(point.y - GetStampHalfSize, 0, renderTexture.height);
            int yTop = Mathf.Clamp(point.y + GetStampHalfSize, 0, renderTexture.height);
            
            int width = xRight - xLeft;
            int height = yTop - yDown;
            
            Color[] stampColors = new Color[(width) * (height)];

            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                int x = point.x - GetStampHalfSize + i;
                int y = point.y - GetStampHalfSize + j;
                float t = stampTexture.GetPixel(i, j).a;
                stampColors[j * width + i] = (1 - t) * _texture.GetPixel(x, y);
            }

            _texture.SetPixels(xLeft, yDown, width, height, stampColors);
        }

        _texture.Apply();
        RenderTexture.active = null;
    }
}
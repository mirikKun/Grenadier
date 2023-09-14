using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Target : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture; // renderTextuer that you will be rendering stuff on
    [SerializeField] private Renderer renderer; // renderer in which you will apply changed texture
    [SerializeField] private Texture2D stampTexture; //Texture to Graphics.Drawtexture on my RenderTexture.

    [SerializeField] private Vector2Int position = new Vector2Int(1024, 1024);
    [SerializeField] private Color backColor = Color.white;
    private Texture2D _texture;
    private int GetStampHalfSize => stampTexture.width / 2;
    private List<Vector2Int> _points=new List<Vector2Int>();
    private bool _updated;

    private void Start()
    {
        _texture = new Texture2D(renderTexture.width, renderTexture.height);
        renderer.material.mainTexture = _texture;

        RenderTexture.active = renderTexture;
    }

    public void AddPoint(Vector2 vector2)
    {
        Debug.Log(vector2);

        Vector2Int intVector = new Vector2Int((int)(vector2.x*renderTexture.width), (int)(vector2.y*renderTexture.width));
        _points.Add(intVector);
        _updated = true;
    }

    private void Update()
    {
        if (_updated)
        {
            Rerender();
            _updated = false;
        }
    }

    private void Rerender()
    {
        RenderTexture.active = renderTexture; 

        _texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        for (int i = 0; i < renderTexture.width; i++)
        for (int j = 0; j < renderTexture.height; j++)
        {
            Color pointColor = backColor;
            foreach (var point in _points)
            {
                if (i> point.x - GetStampHalfSize && i < point.x + GetStampHalfSize 
                 && j > point.y - GetStampHalfSize && j < point.y + GetStampHalfSize)
                {

                    float t = stampTexture.GetPixel(i-(point.x - GetStampHalfSize), j-(point.y - GetStampHalfSize)).a;
                    pointColor = (1 - t) * pointColor;
                    
                }
            }
            _texture.SetPixel(i, j,pointColor);

        }
        _texture.Apply();
        RenderTexture.active = null; //don't forget to set it back to null once you finished playing with it. 
    }
}
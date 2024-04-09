using System;
using UnityEngine;

[System.Serializable] // Make it visible in the Unity Editor.
public class ImageData
{
    public Texture2D texture;
    public bool isActive;
    public string persistentPath;
    public string id; 

    public ImageData(Texture2D texture, bool isActive = true)
    {
        this.texture = texture;
        this.isActive = isActive;
    }
    // Utility to get a Sprite from the texture
    public Sprite GetSprite()
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}

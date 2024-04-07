using UnityEngine;

[System.Serializable] // Make it visible in the Unity Editor.
public class ImageData
{
    public Sprite image;
    public bool isActive;

    public ImageData(Sprite image, bool isActive = true)
    {
        this.image = image;
        this.isActive = isActive;
    }
}

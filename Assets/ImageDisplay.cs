using UnityEngine.UI;
using UnityEngine;

public class ImageDisplay : MonoBehaviour
{
    public ImageData imageData;

    // Method to set the image and its data
    public void SetImage(ImageData data)
    {
        GetComponent<RawImage>().texture = data.texture;
        this.imageData = data;
    }
}

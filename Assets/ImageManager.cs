using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    // Updated to use the wrapper class
    public List<ImageData> imagesData = new List<ImageData>();

    void Start()
    {
        LoadImagesFromResources();
    }

    void LoadImagesFromResources()
    {
        Object[] loadedObjects = Resources.LoadAll($"gridImages", typeof(Sprite));
        foreach (var loadedObject in loadedObjects)
        {
            // Wrap each Sprite with ImageData, defaulting to active.
            imagesData.Add(new ImageData(loadedObject as Sprite));
        }

        // Debug: Print the names of loaded images.
        foreach (ImageData imageData in imagesData)
        {
            Debug.Log($"Loaded image: {imageData.image.name}, Active: {imageData.isActive}");
        }
    }

    // Toggle the active state of an image by name.
    public void ToggleImageActiveState(string imageName)
    {
        foreach (ImageData imageData in imagesData)
        {
            if (imageData.image.name == imageName)
            {
                imageData.isActive = !imageData.isActive;
                Debug.Log($"Image '{imageName}' is now {(imageData.isActive ? "active" : "inactive")}");
                return;
            }
        }
        Debug.Log($"Image not found: {imageName}");
    }
}

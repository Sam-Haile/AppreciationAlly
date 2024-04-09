using System.IO;
using UnityEngine;

public class AddImages : MonoBehaviour
{
    public void OpenGallery()
    {
        // Check if picking media is already in progress
        if (NativeGallery.IsMediaPickerBusy())
            return;

        // Request permission to access the gallery
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Load the selected image as a Texture2D
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 1024); 
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                UsePlayersTexture(texture, path);
            }
        }, "Select an image", "image/*");

        Debug.Log("Permission result: " + permission);
    }

    private void UsePlayersTexture(Texture2D texture, string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Path is null or empty.");
            return;
        }

        // Generate a unique filename
        string fileName = Path.GetFileName(path);
        string uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{System.DateTime.Now.ToString("yyyyMMddHHmmssffff")}{Path.GetExtension(fileName)}";
        string savePath = Path.Combine(Application.persistentDataPath, uniqueFileName);

        // Create a new readable Texture2D to copy the original texture's pixels
        Texture2D readableTexture = new Texture2D(texture.width, texture.height);
        RenderTexture currentRT = RenderTexture.active; // Store current active render texture
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height);
        Graphics.Blit(texture, renderTexture);
        readableTexture.name = fileName;

        RenderTexture.active = renderTexture;
        readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = currentRT; // Reset active render texture
        RenderTexture.ReleaseTemporary(renderTexture); // Clean up temporary render texture

        // Use the readableTexture to encode to PNG
        File.WriteAllBytes(savePath, readableTexture.EncodeToPNG());

        // Proceed with your original logic, using the new texture
        ImageData newImage = new ImageData(readableTexture, true);
        newImage.persistentPath = savePath;
        ImageManager.imagesData.Add(newImage);
        FindObjectOfType<PopulateScrollView>().RefreshGallery();
    }



}

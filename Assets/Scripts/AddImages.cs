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
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 1024); // Adjust maxSize to your needs
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // TODO: Implement logic to use the texture in your game
                UseTexture(texture);
            }
        }, "Select an image", "image/*");

        Debug.Log("Permission result: " + permission);
    }

    private void UseTexture(Texture2D texture)
    {
        // Implement how the texture will be used in your game.
        // For example, you might want to apply it to a GameObject to display the image.
    }

}

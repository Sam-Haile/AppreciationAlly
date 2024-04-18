using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public RawImage image;
    public AspectRatioFitter imageFitter;
    public Button captureButton; // Assign this in the Unity Editor
    public ImageManager imgManager;

    WebCamTexture camTexture;

    void Start()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            camTexture = new WebCamTexture();
            image.texture = camTexture;
            image.material.mainTexture = camTexture;
            camTexture.Play();
            UpdateCameraRender();

            captureButton.onClick.AddListener(CaptureAndSaveImage);

        }
    }

    void UpdateCameraRender()
    {
        if (camTexture.width < 100)
        {
            return;
        }

        float aspectRatio = (float)camTexture.width / (float)camTexture.height;
        imageFitter.aspectRatio = aspectRatio;
        image.uvRect = camTexture.videoVerticallyMirrored ? new Rect(1, 0, -1, 1) : new Rect(0, 0, 1, 1);
    }

    void CaptureAndSaveImage()
    {
        Texture2D photo = new Texture2D(camTexture.width, camTexture.height);
        photo.SetPixels(camTexture.GetPixels());
        photo.Apply();

        // Encode texture into PNG
        byte[] bytes = photo.EncodeToPNG();
        string filename = Path.Combine(Application.persistentDataPath, "capturedImage_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
        File.WriteAllBytes(filename, bytes);

        Destroy(photo); // Free memory

        Debug.Log("Captured photo saved as: " + filename);

        // Optionally, update your gallery immediately
        imgManager.LoadUserImagesFromPersistentData();
        imgManager.RefreshImagesUI();
    }
}

using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class CameraScript : MonoBehaviour
{
    public RawImage image;
    public AspectRatioFitter imageFitter;
    public ImageManager imgManager;

    WebCamTexture camTexture;
    public Quaternion baseRotation;
    WebCamDevice[] devices;
    private bool isImageInverted = false;  // New boolean to track inversion state

    void Start()
    {
        devices = WebCamTexture.devices;
    }

    void UpdateCameraRender(Rect rectangle)
    {
        if (camTexture.width < 100)
        {
            return;
        }

        float aspectRatio = (float)camTexture.width / (float)camTexture.height;
        imageFitter.aspectRatio = aspectRatio;
        image.uvRect = rectangle;
    }

    public void CaptureAndSaveImage()
    {
        Texture2D photo = new Texture2D(camTexture.width, camTexture.height);
        photo.SetPixels(camTexture.GetPixels());
        photo.Apply();

        Texture2D rotatedImg = RotateTexture(photo, 90);

        // Encode texture into PNG
        byte[] bytes = rotatedImg.EncodeToPNG();
        string filename = Path.Combine(Application.persistentDataPath, "capturedImage_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
        File.WriteAllBytes(filename, bytes);

        Destroy(photo); // Free memory
        Destroy(rotatedImg);

        // Optionally, update your gallery immediately
        imgManager.LoadUserImagesFromPersistentData();
        imgManager.RefreshImagesUI();
    }

    private Texture2D RotateTexture(Texture2D originalTexture, float angle)
    {
        int width = originalTexture.width;
        int height = originalTexture.height;

        Texture2D rotatedTexture = new Texture2D(height, width);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                rotatedTexture.SetPixel(y, width - x - 1, originalTexture.GetPixel(x, y));
            }
        }

        rotatedTexture.Apply();
        return rotatedTexture;

    }

    public void OnCameraClick()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            if (camTexture == null)
            {
                camTexture = new WebCamTexture();

            }

            image.texture = camTexture;
            camTexture.filterMode = FilterMode.Trilinear;
            CheckCamera();
            camTexture.Play();
        }
    }

    private void CheckCamera()
    {
        bool isFrontCamera = camTexture.deviceName == WebCamTexture.devices.FirstOrDefault(d => d.isFrontFacing).name;
        if (isFrontCamera)
        {
            Debug.Log("This is the front camera");
            // Front camera, apply horizontal flip for mirror effect
            Rect uvRect = new Rect(0, 0, 1, 1);
            UpdateCameraRender(uvRect);
        }
        else
        {
            Debug.Log("This is the rear camera");
            // Rear camera, usually no need to flip, unless explicitly inverted
            //Rect uvRect = new Rect(0, 0, -1, 1);
            //UpdateCameraRender(uvRect);
        }
    }


    public void ReverseCamera()
    {
        if (devices.Length > 1)
        {
            camTexture.Stop();
            camTexture.deviceName = (camTexture.deviceName == devices[0].name) ? devices[1].name : devices[0].name;
            CheckCamera();
            camTexture.Play();
        }
    }

    public void TurnCameraOff()
    {
        camTexture.Stop();
    }

}

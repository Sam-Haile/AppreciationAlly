using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public RawImage image;
    public AspectRatioFitter imageFitter;
    public ImageManager imgManager;

    WebCamTexture camTexture;
    public Quaternion baseRotation;
    WebCamDevice[] devices; 

    void Start()
    {
        devices = WebCamTexture.devices;
    }

    void UpdateCameraRender(bool mirror)
    {
        if (camTexture.width < 100)
        {
            return;
        }

        float aspectRatio = (float)camTexture.width / (float)camTexture.height;
        imageFitter.aspectRatio = aspectRatio;

        if (mirror)
        {
            image.uvRect = new Rect(1, 0, -1, 1); 

        }
        else
        {
            image.uvRect = new Rect(0, 0, 1, 1);

        }
    }

    void Update()
    {
        //if(camTexture != null)
          //image.transform.rotation = baseRotation * Quaternion.AngleAxis(camTexture.videoRotationAngle, Vector3.up);
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
            camTexture = new WebCamTexture();
            image.texture = camTexture;
            camTexture.filterMode = FilterMode.Trilinear;
            camTexture.Play();
            UpdateCameraRender(true);
        }

    }



    public void ReverseCamera()
    {
        if(devices.Length > 1)
        {
            camTexture.Stop();
            if(devices.Length > 1)
            {
                camTexture.Stop();
                camTexture.deviceName = (camTexture.deviceName == devices[0].name) ? devices[1].name : devices[0].name;
                camTexture.Play();
                UpdateCameraRender(false);
            }

        }
    }

    public void TurnCameraOff()
    {
        camTexture.Stop();
    }
}

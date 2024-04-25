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
        camTexture = new WebCamTexture();

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

    void Update()
    {
        image.transform.rotation = baseRotation * Quaternion.AngleAxis(camTexture.videoRotationAngle, Vector3.up);
    }


    public void CaptureAndSaveImage()
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

    public void OnCameraClick()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            image.texture = camTexture;
            camTexture.filterMode = FilterMode.Trilinear;
            camTexture.Play();
            UpdateCameraRender();
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
            }

        }
    }

}

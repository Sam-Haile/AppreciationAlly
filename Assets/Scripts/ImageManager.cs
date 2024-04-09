using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    // Updated to use the wrapper class
    public static List<ImageData> imagesData = new List<ImageData>();
    public List<ImageData> temp = new List<ImageData>();

    public RawImage selectedImg;
    public string selectedImgId;
    public PopulateScrollView img;

    void Start()
    {
        imagesData.Clear();
        LoadUserImagesFromPersistentData();
        LoadImagesFromResources();
        LoadImageStates();
        temp = imagesData;
    }

    void LoadImagesFromResources()
    {
        Object[] loadedObjects = Resources.LoadAll($"gridImages", typeof(Texture2D));
        foreach (var loadedObject in loadedObjects)
        {
            Texture2D texture = loadedObject as Texture2D;
            if (texture != null)
            {
                // Here, we're assuming the name of the texture (its asset name) is unique and can serve as an ID.
                string id = texture.name;
                imagesData.Add(new ImageData(texture) { id = id });
            }
        }
    }

    // Method to load user images from the application's persistent data path
    void LoadUserImagesFromPersistentData()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo = info.GetFiles().Where(f => f.Extension.Equals(".png") || f.Extension.Equals(".jpg") || f.Extension.Equals(".jpeg")).ToArray();
        foreach (var file in fileInfo)
        {
            byte[] fileData = File.ReadAllBytes(file.FullName);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData)) // This method auto-resizes the texture dimensions.
            {
                // Using the file name as the ID. Ensure filenames are unique or use a different method for generating IDs.
                string id = Path.GetFileNameWithoutExtension(file.Name);
                imagesData.Add(new ImageData(texture) { persistentPath = file.FullName, id = id });
            }
        }
    }

    // Toggle the active state of an image by id.
    public void ToggleImageActiveState(bool state)
    {
        foreach (ImageData imageData in imagesData)
        {
            if (imageData.id == selectedImgId)
            {
                imageData.isActive = state;

                if (state == false)
                    img.originalImage.color = new Color(.5f, .5f, .5f, 1f);
                else
                    img.originalImage.color = new Color(1f, 1f, 1f, 1f);

                //Debug.Log($"Image '{selectedImg.texture.name}' is now {(imageData.isActive ? "active" : "inactive")}");
                SaveImageStates();
                return;
            }
        }

        Debug.Log($"Image not found: {selectedImg.texture.name}");

    }

    public static void SaveImageStates()
    {
        for (int i = 0; i < imagesData.Count; i++)
        {
            // Save each image's active state as an int (1 for active, 0 for inactive)
            PlayerPrefs.SetInt($"ImageDataIsActive_{i}", imagesData[i].isActive ? 1 : 0);
        }

        // It's important to save PlayerPrefs changes.
        PlayerPrefs.Save();
    }

    public static void LoadImageStates()
    {
        for (int i = 0; i < imagesData.Count; i++)
        {
            // Load the active state for each image, defaulting to 1 (true) if not found.
            int isActive = PlayerPrefs.GetInt($"ImageDataIsActive_{i}", 1);
            imagesData[i].isActive = isActive == 1;
        }
    }

}

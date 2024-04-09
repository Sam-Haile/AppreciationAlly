using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollView : MonoBehaviour
{
    public GameObject imagePrefab; // Assign a prefab with an Image component in the editor
    public GameObject selectedImageObj;
    public RawImage selectedImage;

    public RawImage originalImage;

    void Start()
    {
        Populate();
    }

    /// <summary>
    /// Display the images in the gallery 
    /// </summary>
    private void Populate()
    {
        for (int i = 0; i < ImageManager.imagesData.Count; i++)
        {
            GameObject imageObj = Instantiate(imagePrefab, transform, false);
            ImageDisplay display = imageObj.GetComponent<ImageDisplay>();
            
            if(display != null)
            {
                display.SetImage(ImageManager.imagesData[i]);
            }

            if (PlayerPrefs.GetInt($"ImageDataIsActive_{i}", 1) == 0)
            {
                imageObj.GetComponent<RawImage>().color = new Color(.5f, .5f, .5f, 1f);
            }

            imageObj.GetComponent<RawImage>().texture = ImageManager.imagesData[i].texture;
            imageObj.transform.SetParent(gameObject.transform, false);
        }
    }

    public void RefreshGallery()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Clean up old gallery items
        }
        Populate(); // Repopulate the gallery
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

public class PopulateScrollView : MonoBehaviour
{
    public GameObject imagePrefab; // Assign a prefab with an Image component in the editor
    public ImageManager imageManager;
    public GameObject selectedImageObj;
    public RawImage selectedImage;


    void Start()
    {
        Populate();
    }

    private void Populate()
    {
        foreach (ImageData sprite in imageManager.imagesData)
        {
            GameObject imageObj = Instantiate(imagePrefab, transform, false);
            imageObj.GetComponent<RawImage>().texture = sprite.image.texture;
            imageObj.transform.SetParent(gameObject.transform, false);
        }
    }


}

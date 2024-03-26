using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollView : MonoBehaviour
{
    public GameObject imagePrefab; // Assign a prefab with an Image component in the editor
    public List<Sprite> loadedSprites; // Your preloaded images

    void Start()
    {
        Populate();
    }

    void Populate()
    {
        foreach (Sprite sprite in loadedSprites)
        {
            GameObject imageObj = Instantiate(imagePrefab, transform, false);
            imageObj.GetComponent<Image>().sprite = sprite;
            imageObj.transform.SetParent(gameObject.transform, false);
        }
    }
}

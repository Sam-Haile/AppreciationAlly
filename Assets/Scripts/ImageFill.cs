using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageFill : MonoBehaviour
{
    RectTransform rt;
    RawImage img;
    Rect lastBounds;
    Texture lastTexture;
    public ImageManager manager;

    private void Awake()
    {
        GameObject obj = GameObject.Find("ImageManager");
        if(obj != null)
        {
            manager = obj.GetComponent<ImageManager>();
        }
    }

    void Update()
    {
        if (rt == null) rt = transform as RectTransform;
        if (img == null) img = GetComponent<RawImage>();

        if ((rt != null && rt.rect != lastBounds) ||
            (img != null && img.mainTexture != lastTexture)) UpdateUV();
    }

    public void UpdateUV()
    {
        if (rt == null || img == null) return;
        lastBounds = rt.rect;
        float frameAspect = lastBounds.width / lastBounds.height;

        lastTexture = img.mainTexture;
        float imageAspect = (float)lastTexture.width / (float)lastTexture.height;

        if (frameAspect == imageAspect)
        {
            img.uvRect = new Rect(0, 0, 1, 1);
        }
        else if (frameAspect < imageAspect)
        {
            float w = frameAspect / imageAspect;
            img.uvRect = new Rect(0.5f - w * 0.5f, 0, w, 1);
        }
        else
        {
            float h = imageAspect / frameAspect;
            img.uvRect = new Rect(0, 0.5f - h * 0.5f, 1, h);
        }
    }

    public void OnImageSelected()
    {
        PopulateScrollView obj = GetComponentInParent<PopulateScrollView>();
        obj.selectedImage.texture = this.GetComponent<RawImage>().texture;
        obj.selectedImageObj.SetActive(true);
        obj.originalImage = this.GetComponent<RawImage>();
        manager.selectedImgId = this.GetComponent<ImageDisplay>().imageData.id;

        bool isImgUserAdded = this.GetComponent<ImageDisplay>().imageData.userAdded;
        if (isImgUserAdded)
            obj.trashButton.SetActive(true);
        else
            obj.trashButton.SetActive(false);
    }

}

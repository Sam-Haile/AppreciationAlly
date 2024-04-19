using System.Collections;
using UnityEngine;

public class UpdateColor : MonoBehaviour
{

    public GameObject[] colorOptions;

    private UnityEngine.Color selectedPrimaryColor;
    private UnityEngine.Color selectedSecondaryColor;

    public void Start()
    {
        string originalColor = PlayerPrefs.GetString("PrimaryColor", "0E46A7");
        originalColor = originalColor.Substring(0, originalColor.Length - 2);

        
        foreach(var colorOption in colorOptions)
        {
            if(colorOption.GetComponent<BackgroundColor>().primaryColor == originalColor)
            {
                colorOption.transform.localScale = new Vector3(1,1,1);
            }
        }
    }


    public void OnClick(BackgroundColor selectedColor)
    {
        SetColor(selectedColor);

        PlayerPrefs.SetString("PrimaryColor", ColorUtility.ToHtmlStringRGBA(selectedPrimaryColor));
        PlayerPrefs.SetString("SecondaryColor", ColorUtility.ToHtmlStringRGBA(selectedSecondaryColor));
        PlayerPrefs.Save();

        LoadPreferences.ApplyColors();
    }

    public void SetColor(BackgroundColor color)
    {

        ColorUtility.TryParseHtmlString("#" + color.primaryColor, out selectedPrimaryColor);
        ColorUtility.TryParseHtmlString("#" + color.secondaryColor, out selectedSecondaryColor);


        foreach (var colorOption in colorOptions)
        {
            if (colorOption.GetComponent<BackgroundColor>().primaryColor != color.primaryColor)
                StartCoroutine(InterpolateScale(colorOption, new Vector3(.75f, .75f, 1f), .15f));
            else
                StartCoroutine(InterpolateScale(colorOption, new Vector3(1f, 1f, 1f), .15f));
        }

    }


    private IEnumerator InterpolateScale(GameObject objToTransform, Vector3 targetScale, float duration)
    {
        Vector3 originalScale = objToTransform.transform.localScale;
        float timer = 0;

        while (timer < duration)
        {
            objToTransform.transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is set to the exact target scale at the end
        objToTransform.transform.localScale = targetScale;

    }


}

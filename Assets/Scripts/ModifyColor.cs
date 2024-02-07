using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyColor : MonoBehaviour
{

    public float duration = 30f;
    public Material chromoColor;
    bool changeColor;

    private void Update()
    {
        if (!changeColor && chromoColor != null)
        {
            StartCoroutine(ChangeColor(duration));
        }
    }

    IEnumerator ChangeColor(float duration)
    {
        changeColor = true;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float normalizedTime = time / duration;
            chromoColor.color = Color.HSVToRGB(normalizedTime, 1f, 1f);
            yield return null;
        }

        changeColor = false;
    }

}

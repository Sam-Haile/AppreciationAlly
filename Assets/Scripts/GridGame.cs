using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridGame : MonoBehaviour
{
    public GameObject gridImage;
    public GameObject gameOverUI;

    //Grid fields
    public Sprite[] images;
    public RawImage[] grids;
    public GameObject[] gridObjs;
    public Slider progressSlider;
    public int numOfGrids;
    private int selectedGridCounter;

    public RawImage enlargedGrid;
    public GameObject enlargedGridParent;
    //UI Color Application
    public GameObject[] inactiveObjects;

    private int selectedGrid;

    private Color parsedSecondaryColor;
    private Color parsedPrimaryColor;

    public GameObject[] gridNumSelector;


    private void Start()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PrimaryColor"), out parsedPrimaryColor);
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("SecondaryColor"), out parsedSecondaryColor);

        RandomizeImages();
        PopulateGrid();
    }

    public void OnNumberSelected(GameObject button)
    {
        foreach (GameObject obj in gridNumSelector)
        {
            if (obj == button)
            {
                obj.GetComponent<Image>().color = parsedPrimaryColor;
                int.TryParse(obj.name, out numOfGrids);
            }
            else
            {
                obj.GetComponent<Image>().color = parsedSecondaryColor;
            }
        }
    }

    #region Populate/Randomize Grid
    //Utilizing the fisher-yates system to randomize the first 16 elements
    private void RandomizeImages()
    {
        int n = 4;

        for (int i = 0; i < n; i++)
        {
            int j = Random.Range(i, images.Length);

            Swap(i, j);
        }
    }

    private void Swap(int i, int j)
    {
        Sprite temp = images[i];
        images[i] = images[j];
        images[j] = temp;
    }

    public void PopulateGrid()
    {
        for (int i = 0; i < grids.Length; i++)
        {
            grids[i].texture = images[i].texture;
        }
    }
    #endregion

    public void SetSelectedGrid(int gridIndex)
    {
        selectedGrid = gridIndex;
        Debug.Log($"Setting selected grid: {selectedGrid}");

        Texture texture = grids[selectedGrid].texture;
        Debug.Log($"Selected texture: {texture.name}");

        enlargedGrid.texture = texture;
    }

    public void SetObjectInactive()
    {
        foreach (GameObject obj in inactiveObjects)
        {
            obj.SetActive(false);
        }
    }

    public void OnTap()
    {
        StartCoroutine(ModifySizes(gridObjs, enlargedGridParent));
        RandomizeImages();
        PopulateGrid();

    }

    public void OnEnlargedImageTapped()
    {
        selectedGridCounter++;

        if (selectedGridCounter < numOfGrids)
        {
            StartCoroutine(SmoothProgressChange(progressSlider.value, (float)selectedGridCounter / (float)numOfGrids, 0.25f));
            StartCoroutine(NextGrids(gridObjs, enlargedGridParent, true));
            RandomizeImages();
            PopulateGrid();
        }
        else
        {
            StartCoroutine(NextGrids(gridObjs, enlargedGridParent, false));
            EndGame();
        }
    }



    IEnumerator SmoothProgressChange(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            progressSlider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        progressSlider.value = endValue; // Ensure the slider reaches the final value
    }


    /// <summary>
    /// This method shrinks the grid of images once one is selected
    /// It also enlarges the selected images, allowing the user to view it
    /// </summary>
    /// <param name="objsToShrink"></param>
    /// <param name="objToEnlarge"></param>
    /// <returns></returns>
    IEnumerator ModifySizes(GameObject[] gridImages, GameObject enlargedImage)
    {
        float elapsedTime = 0;

        while (elapsedTime < .20f)
        {
            //Shrink all grid images
            foreach (GameObject obj in gridImages)
            {
                obj.transform.localScale = new Vector2(Mathf.Lerp(1, 0, elapsedTime / .20f), Mathf.Lerp(1, 0, elapsedTime / .20f));
                obj.SetActive(false);
            }

            enlargedImage.SetActive(true);
            enlargedImage.transform.localScale = new Vector2(Mathf.Lerp(0, 1, elapsedTime / .20f), Mathf.Lerp(0, 1, elapsedTime / .20f));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator NextGrids(GameObject[] gridImages, GameObject enlargedImage, bool enlarge)
    {
        float elapsedTime = 0;

        while (elapsedTime < .20f)
        {
            if (enlarge)
            {

                foreach (GameObject obj in gridImages)
                {
                    obj.SetActive(true);
                    obj.transform.localScale = new Vector2(Mathf.Lerp(0, 1, elapsedTime / .20f), Mathf.Lerp(0, 1, elapsedTime / .20f));
                }
            }

            enlargedImage.transform.localScale = new Vector2(Mathf.Lerp(1, 0, elapsedTime / .20f), Mathf.Lerp(1, 0, elapsedTime / .20f));
            enlargedImage.SetActive(false);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    public void EndGame()
    {
        progressSlider.value = 1;
        gameOverUI.SetActive(true);
        DailyTasks.Instance.SaveTask("GridGame");
    }

    public void RestartGame()
    {
        PopulateGrid();
        selectedGridCounter = 0;
        progressSlider.value = 0;
    }

}

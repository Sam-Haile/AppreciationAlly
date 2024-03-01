using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridGame : MonoBehaviour
{
    public GameObject gridImage;
    public GameObject gameOverUI;
    public Canvas canvas;

    //Grid fields
    public Sprite[] images;
    public RawImage[] grids;
    public GameObject[] gridObjs;
    public Slider progressSlider;
    public int numOfGrids;
    private int selectedGridCounter;

    //UI Color Application
    public GameObject[] inactiveObjects;

    private int selectedGrid;

    private void Start()
    {
        RandomizeImages();
        PopulateGrid();
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
        Debug.Log(gridObjs[selectedGrid]);
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
        selectedGridCounter++;

        if (selectedGridCounter < numOfGrids)
        {
            StartCoroutine(SmoothProgressChange(progressSlider.value, (float)selectedGridCounter / (float)numOfGrids, 0.25f));
            foreach (GameObject obj in gridObjs)
                StartCoroutine(ShrinkGrids(obj));
            SetSelectedImage();
            RandomizeImages();
            PopulateGrid();
        }
        else
        {
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

    private void SetSelectedImage()
    {

    }

    IEnumerator ShrinkGrids(GameObject obj)
    {
        float elapsedTime = 0;

        while (elapsedTime < .20f)
        {
            obj.transform.localScale = new Vector2(Mathf.Lerp(1, 0, elapsedTime/ .20f), Mathf.Lerp(1, 0, elapsedTime/ .20f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator EnlargeSelection(GameObject obj)
    {
        float elapsedTime = 0;

        while (elapsedTime < .20f)
        {
            obj.transform.localScale = new Vector2(Mathf.Lerp(0, 1, elapsedTime / .20f), Mathf.Lerp(0, 1, elapsedTime / .20f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void EndGame()
    {
        progressSlider.value = 1;
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        selectedGridCounter = 0;
        progressSlider.value = 0;
    }

}

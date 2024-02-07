using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGame : MonoBehaviour
{
    public GameObject gridImage;
    public Canvas canvas;

    private int screenWidth;
    private int screenHeight;

    private void Start()
    {
        screenWidth = Screen.width; 
        screenHeight = Screen.height;

        Spawn();
    }


    private void Spawn()
    {
        Vector3 spawnPosition = new Vector3(0, Camera.main.orthographicSize + gridImage.GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
        
        GameObject spawnedBackground = Instantiate(gridImage, spawnPosition, Quaternion.identity);

        float randomScale = Random.Range(.45f, 1f);

        spawnedBackground.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

}

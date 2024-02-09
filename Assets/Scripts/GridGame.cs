using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridGame : MonoBehaviour
{
    public GameObject gridImage;
    public Canvas canvas;

    //Grid fields
    public Sprite[] images;
    public RawImage[] grids;
    public Slider progressSlider;
    public int numOfGrids;
    private int selectedGridCounter;

    // Score fields
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public int score;


    //Timer fields
    public TextMeshProUGUI timerText;
    public GameObject timerGameObj;
    private float timer = 120.0f;
    private float timeRemaining;
    private bool timerIsRunning = false;
    public bool playWithTimer;


    private void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        RandomizeImages();
        PopulateGrid();
    }

    public void PlayWithTimer()
    {
        timeRemaining = timer;
        selectedGridCounter = 0;
        timerIsRunning = true;
    }


    public void TurnTimerOnOff(bool isOn)
    {
        timerGameObj.SetActive(isOn);
    }

    private void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining, timerText);
            }
            else
            {
                timerText.text = "0.00";
                Debug.Log("Done");
            }
        }
        
    }


    private void DisplayTime(float timeRemaining, TextMeshProUGUI text)
    {
        timeRemaining += 1;

        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        string formattedTime = minutes + ":" + seconds.ToString("00");

        text.text = formattedTime;
    }

    //Utilizing the fisher-yates system to randomize the first 16 elements
    private void RandomizeImages()
    {
        int n = 16;

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
        for(int i = 0; i < grids.Length;i++)
        {
            grids[i].texture = images[i].texture;
        }
    }


    public void OnTap()
    {
        selectedGridCounter++;


        if (selectedGridCounter <= numOfGrids)
        {
            progressSlider.value = (float)selectedGridCounter / (float)numOfGrids;
            score += 100;
            scoreText.text = "SCORE: " + score.ToString();
            RandomizeImages();
            PopulateGrid();
        }
        else{
            progressSlider.value = 1;
            int currentHighscore = PlayerPrefs.GetInt("Highscore", 0);

            if(currentHighscore < score)
            {
                PlayerPrefs.SetInt("Highscore", score);
                highscoreText.text = score.ToString();
            }
        }


    }
    

}

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
    public Slider progressSlider;
    public int numOfGrids;
    private int selectedGridCounter;

    // Score fields
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI gameEndScoreText;
    public TextMeshProUGUI gameEndHighscoreText;
    public int score;
    public int highscore;

    //Timer fields
    public TextMeshProUGUI timerText;
    public GameObject timerGameObj;
    private float timer = 120.0f;
    private float timeRemaining;
    private bool timerIsRunning = false;
    public bool playWithTimer;

    //UI Color Application
    public GameObject[] inactiveObjects;

    private void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        highscore = PlayerPrefs.GetInt("Highscore", 0);
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

    public void SetObjectInactive()
    {
        foreach (GameObject obj in inactiveObjects)
        {
            obj.SetActive(false);
        }
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

    public void SetTimerActive(bool isActive)
    {
        timerIsRunning=isActive;
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

        if (selectedGridCounter < numOfGrids)
        {
            StartCoroutine(SmoothProgressChange(progressSlider.value, (float)selectedGridCounter / (float)numOfGrids, 0.25f));
            score += 100;
            scoreText.text = "SCORE: " + score.ToString();
            RandomizeImages();
            PopulateGrid();

            if (highscore < score)
            {
                highscoreText.text = score.ToString();
            }
        }
        else{

            score += 100;
            scoreText.text = "SCORE: " + score.ToString();
            EndGame();
        }


    }

    public void EndGame()
    {
        progressSlider.value = 1;

        if (highscore < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = score.ToString();
        }

        gameEndHighscoreText.text = highscoreText.text;
        gameEndScoreText.text = score.ToString();

        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        selectedGridCounter = 0;
        progressSlider.value = 0;
        score = 0;
        scoreText.text = "";
        timer = 120.0f;
        timerText.text = "2:00";
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

}

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class QuoteOfTheDay : MonoBehaviour
{
    public TextMeshProUGUI quoteText; // Assign this in the Unity Editor

    private string apiUrl = "https://positivity-tips.p.rapidapi.com/api/positivity/affirmation";

    // Store your RapidAPI key here (ideally, you should find a more secure way to handle API keys)
    private string apiKey = "cfa5477a8amsheccc1802ecc8849p14fc04jsn76b4de1e37f2";

    private void Start()
    {
        FetchQuote();
    }

    private void FetchQuote()
    {
        StartCoroutine(GetQuoteCoroutine());
    }

    private IEnumerator GetQuoteCoroutine()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl);
        // Set the necessary headers for the API request
        webRequest.SetRequestHeader("X-RapidAPI-Key", apiKey);
        webRequest.SetRequestHeader("X-RapidAPI-Host", "positivity-tips.p.rapidapi.com");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            ProcessQuoteResponse(webRequest.downloadHandler.text);
        }

    }

    private void ProcessQuoteResponse(string jsonResponse)
    {
        QuoteResponse quoteResponse = JsonUtility.FromJson<QuoteResponse>(jsonResponse);

        if (quoteText != null && quoteResponse != null)
        {
            quoteText.text = quoteResponse.affirmation;
        }
    }

    [System.Serializable]
    private class QuoteResponse
    {
        public string affirmation; // Make sure this matches the field from your JSON response
    }
}

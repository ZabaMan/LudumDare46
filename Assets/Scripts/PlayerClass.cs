using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerClass
{
    public string[] input;
    public Color color;
    public string honorific;
    GameObject playerSetup;
    public bool setupComplete;

    public float currentLevelTime;
    public float score;

    public void SetPlayerSetup(GameObject playerSetup)
    {
        this.playerSetup = playerSetup;
    }

    public void AddColorAndHonorific(Color playerColor, string honorific)
    {
        color = playerColor;
        this.honorific = honorific;
    }

    public void AddInput(string[] recordedInput)
    {
        input = recordedInput;
        setupComplete = true;
    }

    public void AddLevelTime(float time)
    {
        currentLevelTime = time;
        score += currentLevelTime;
    }
}

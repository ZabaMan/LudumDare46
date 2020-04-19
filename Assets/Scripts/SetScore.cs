using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public Image image;
    public Text honorificText, nameText, newScoreText, totalScoreText;

    public void Set(Color imageColor, string honorific, string name, string newScore, string totalScore)
    {
        image.color = imageColor;
        honorificText.text = honorific;
        nameText.text = name;
        newScoreText.text = newScore;
        totalScoreText.text = totalScore;
    }
}

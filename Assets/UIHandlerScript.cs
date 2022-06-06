using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandlerScript : MonoBehaviour
{

    public Text scoreText;
    private int score = 0;
    
    public int GetScore()
    {
        return score;
    }

    public void SetScore(int sc)
    {
        score = sc;
        scoreText.text = "Score\n" + score.ToString();
    }
}

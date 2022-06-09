using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IPizzaTowerUIMessageTarget : IEventSystemHandler
{
    // functions that can be called via the messaging system
    void SetScore(int s);
    void SetScoreRequired(int s);
    void IncrementScore(int s);
    void SetLevel(int l);
}

public class UIHandlerScript : MonoBehaviour, IPizzaTowerUIMessageTarget
{

    public Text scoreText;
    public Text levelText;
    private int score = 0;
    private int score_required = 1;
    private int level = 1;

    private void UpdateScoreText()
    {
        scoreText.text = "Score\n" + score.ToString() + "/" +score_required.ToString();
    }
    private void UpdateLevelText()
    {
        levelText.text = "Level\n" + level.ToString();
    }

    public void SetScore(int s)
    {
        score = s;
        UpdateScoreText();
    }
    public void SetScoreRequired(int s)
    {
        score_required = s;
        UpdateScoreText();
    }

    public void IncrementScore(int s)
    {
        score += s;
        UpdateScoreText();
    }

    public void SetLevel(int l)
    {
        level = l;
        UpdateLevelText();
    }
}
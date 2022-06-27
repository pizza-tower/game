using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;

public class Score : MonoBehaviour
{
    public static int CurrentScore;
    public int ScoreToPass = 30;

    // Start is called before the first frame update

    public static void EarnScore()
    {
        CurrentScore += 5;
        GameObject ui_handler = GameObject.Find("UIHandler");
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.IncrementScore(5));
        Vector3 pos;
        pos.x = 0;
        pos.y = 0;
        pos.z = 0;
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.ShowPopupText("Scores+5!", pos));
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Get the score" + CurrentScore);
    }
    void Start()
    {
        CurrentScore = 0;
        GameObject ui_handler = GameObject.Find("UIHandler");
        ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetScoreRequired(30));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

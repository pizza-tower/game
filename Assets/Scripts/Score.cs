using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //public static int CurrentScore;
    public static int numVerticalFuses = 0;
    public static int numHorizontalFuses = 0;
    public static int numPowersUsed = 0;
    public static List<int> fusionsMade;

    // Start is called before the first frame update

    /* public static void EarnScore()
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
    }*/
    void Start()
    {
        fusionsMade = new List<int>();
        for(int i = 0; i < GlobalData.ValidCombinations[SceneManager.GetActiveScene().name].Count; i++)
        {
            fusionsMade.Add(0);
        }
    }

    public void ModVals(int v, int h, int p)
    {
        numVerticalFuses += v;
        numHorizontalFuses += h;
        numPowersUsed += p;
    }

    public ScoreSummary GetScoreSummary()
    {
        ScoreSummary s =  new();
        s.numVerticalFusions = numVerticalFuses;
        s.numHorizontalFusions = numHorizontalFuses;
        s.numPowersUsed = numPowersUsed;
        s.numSlicesLeft = GameObject.Find("PizzaSpawner").GetComponent<NewSliceSpawn>().NumberSpawned;

        s.scoreVerticalFusions = s.numVerticalFusions * 5;
        s.scoreHorizontalFusions = s.numHorizontalFusions * 20;
        s.scorePowersUsed = s.numPowersUsed * 5;
        s.scoreSlicesLeft = s.numSlicesLeft;

        s.scoreTotal = s.scoreVerticalFusions + s.scoreHorizontalFusions + s.scorePowersUsed + s.scoreSlicesLeft;

        s.starsEarned = 1;
        if (s.scoreTotal > 65) s.starsEarned = 2;
        if (s.scoreTotal > 85) s.starsEarned = 3;

        return s;
    }
}

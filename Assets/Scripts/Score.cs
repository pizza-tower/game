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

    void Start()
    {
        numVerticalFuses = 0;
        numHorizontalFuses = 0;
        numPowersUsed = 0;
        fusionsMade = new List<int>();
        for(int i = 0; i < GlobalData.ValidCombinations[SceneManager.GetActiveScene().name].Count; i++)
        {
            fusionsMade.Add(0);
        }
    }

    public static ScoreSummary GetScoreSummary()
    {
        ScoreSummary s =  new();
        s.numVerticalFusions = numVerticalFuses;
        s.numHorizontalFusions = numHorizontalFuses;
        s.numPowersUsed = numPowersUsed;
        s.numSlicesLeft = GlobalData.MaxSlices[SceneManager.GetActiveScene().name] - GameObject.Find("PizzaSpawner").GetComponent<NewSliceSpawn>().NumberSpawned + 1;

        s.scoreVerticalFusions = s.numVerticalFusions * 5;
        s.scoreHorizontalFusions = s.numHorizontalFusions * 20;
        s.scorePowersUsed = s.numPowersUsed * 5;
        s.scoreSlicesLeft = s.numSlicesLeft;

        s.scoreTotal = s.scoreVerticalFusions + s.scoreHorizontalFusions + s.scorePowersUsed + s.scoreSlicesLeft;

        s.starsEarned = 1;
        if (s.scoreTotal > 65) s.starsEarned = 2;
        if (s.scoreTotal > 75) s.starsEarned = 3;

        return s;
    }
}

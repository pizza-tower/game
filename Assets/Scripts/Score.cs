using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int CurrentScore = 0;
    public int ScoreToPass = 30; 
    // Start is called before the first frame update

    public static void EarnScore()
    {
        CurrentScore += 5;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    private int lv_num;
    private int current_score;
    private int score_required;

    public abstract void Setup();
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLevelNumber(int lv)
    {
        lv_num = lv;
    }
    int GetLevelNumber()
    {
        return lv_num;
    }
    void SetCurrentScore(int sc)
    {
        current_score = sc;
    }
    int GetCurrentScore()
    {
        return current_score;
    }
    void SetScoreRequired(int sc)
    {
        score_required = sc;
    }
    int GetScoreRequired()
    {
        return score_required;
    }
}

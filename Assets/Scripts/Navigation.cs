using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public GameObject   winscreen;
    bool instantiated = false;
    void CleanGlobalList()
    {
        GlobalData.globalList = new List<List<GameObject>>();
        for(int i = 0; i < 6; i++)
        {
            GlobalData.globalList.Add(new List<GameObject>());
        }
        
    }
    void Start()
    {
    }
    public void BackToMenu()
    {
        GlobalData.level = 0;
        ResetVariables();
        SceneManager.LoadScene(0);
    }
    public void GoBack() {
        int level = SceneManager.GetActiveScene().buildIndex;
        AnalyticsResult analyticsResult2 = Analytics.CustomEvent("RewardsUsage", new Dictionary<string, object> { { "Level",SceneManager.GetActiveScene().name}, {"RewardsUsage",GlobalData.LevelRewardConsume } });
        GlobalData.LevelRewardConsume = 0;
        if (level > 0) {
            level--;

            ResetVariables();
            GlobalData.level--;
            SceneManager.LoadScene(level);
        }
    }

    public void StartNextLevel() {
        int level = SceneManager.GetActiveScene().buildIndex;
        print(level);
        print("total:");
        print(SceneManager.sceneCount);
        AnalyticsResult analyticsResult1 = Analytics.CustomEvent("Level Win", new Dictionary<string, object> { { "level", level } });
        AnalyticsResult analyticsResult2 = Analytics.CustomEvent("RewardsUsage", new Dictionary<string, object> { { "Level",SceneManager.GetActiveScene().name}, {"RewardsUsage",GlobalData.LevelRewardConsume } });
        GlobalData.LevelRewardConsume = 0;
        if (level + 1 < GlobalData.totalscenes) {
            level++;
            ResetVariables();
            GlobalData.level++;
            SceneManager.LoadScene(level);
        }
        else if(instantiated == false){
            Instantiate(winscreen);
            instantiated = true;
            
        }
    }

    void ResetVariables() {
        GlobalData.isFirstSlice = true;
        GlobalData.previousSlice="AnchorOne";
        GlobalData.isFirstFusionOver = false;
        GlobalData.gameover = false;
        Score.CurrentScore = 0;
        GlobalData.nHorizontalFusions = 0;
        GlobalData.nVerticalFusions = 0;
        //CleanGlobalList();
    }

    public void RestartLevel() {
        int level = SceneManager.GetActiveScene().buildIndex;
        print("restart");
        AnalyticsResult analyticsResult2 = Analytics.CustomEvent("RewardsUsage", new Dictionary<string, object> { { "Level",SceneManager.GetActiveScene().name}, {"RewardsUsage",GlobalData.LevelRewardConsume } });
        GlobalData.LevelRewardConsume = 0;
        ResetVariables();
        SceneManager.LoadScene(level);

    }


    // Update is called once per frame
    void Update()
    {
        if (GlobalData.gameover && instantiated == false) {
            AnalyticsResult analyticsResult = Analytics.CustomEvent("Level Die", new Dictionary<string, object> { { "level", SceneManager.GetActiveScene().buildIndex} });
            Instantiate(menu);
            instantiated = true;
        }
        if(Score.CurrentScore >= 30){
            StartNextLevel();
             }
        } 
    // }
}

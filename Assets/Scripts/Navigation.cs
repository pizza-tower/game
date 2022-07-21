using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using TMPro;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    GameObject plate;
    Score score;
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
        plate = GameObject.FindWithTag("Plate");
        score = plate.GetComponent<Score>();

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

    }

    void ResetVariables() {
        GlobalData.isFirstSlice = true;
        GlobalData.previousSlice="AnchorOne";
        GlobalData.isFirstFusionOver = false;
        GlobalData.gameover = false;
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
            GameObject popup = Instantiate(menu);
            int level = SceneManager.GetActiveScene().buildIndex;

            if (level + 1 >= GlobalData.totalscenes) {
                popup.transform.GetChild(13).gameObject.SetActive(false);
            }
            instantiated = true;
            GameObject star1 = popup.transform.GetChild(1).gameObject;
            GameObject star2 = popup.transform.GetChild(2).gameObject;
            GameObject star3 = popup.transform.GetChild(3).gameObject;
            GameObject plate = GameObject.FindWithTag("Plate");
            ScoreSummary s = Score.GetScoreSummary();
            print("STARS");
            print(s.starsEarned);
            if (s.starsEarned >= 1) {
                star1.SetActive(true);
            }
            if (s.starsEarned >= 2) {
                star2.SetActive(true);
            }
            if (s.starsEarned >= 3) {
                star3.SetActive(true);
            }
            Transform vertscore = popup.transform.GetChild(4);
            TextMeshProUGUI verttext = vertscore.gameObject.GetComponent<TextMeshProUGUI>();
            verttext.SetText(string.Format("# Vertical Fusions = {0} x 5 = {1} points", s.numVerticalFusions, s.scoreVerticalFusions));    

            Transform horscore = popup.transform.GetChild(5);
            TextMeshProUGUI hortext = horscore.gameObject.GetComponent<TextMeshProUGUI>();
            hortext.SetText(string.Format("# Horizontal Fusions = {0} x 20 = {1} points", s.numHorizontalFusions, s.scoreHorizontalFusions));    

            Transform powscore = popup.transform.GetChild(6);
            TextMeshProUGUI powtext = powscore.gameObject.GetComponent<TextMeshProUGUI>();
            powtext.SetText(string.Format("# Powers Used = {0} x 5 = {1} points", s.numPowersUsed, s.scorePowersUsed));    

            Transform slicescore = popup.transform.GetChild(7);
            TextMeshProUGUI slicetext = slicescore.gameObject.GetComponent<TextMeshProUGUI>();
            slicetext.SetText(string.Format("# Slices Left = {0} x 1 = {1} points", s.numSlicesLeft, s.scoreSlicesLeft));    

            Transform totalscore = popup.transform.GetChild(8);
            TextMeshProUGUI totaltext = totalscore.gameObject.GetComponent<TextMeshProUGUI>();
            totaltext.SetText(string.Format("Total Score = {0} points", s.scoreTotal));    
        }
        /*if(Score.CurrentScore >= 30){
            int level = SceneManager.GetActiveScene().buildIndex;
            if (level + 1 >= GlobalData.totalscenes) {
                if(instantiated == false){
                    GameObject popup = Instantiate(winscreen);
                    instantiated = true;
                    Transform finalscore = popup.transform.GetChild(1);
                    TextMeshProUGUI scoretext = finalscore.gameObject.GetComponent<TextMeshProUGUI>();
                    scoretext.SetText("score: {0}", Score.CurrentScore);    
                
                }
            }
            else {
                if (instantiated == false) {
                    GameObject popup = Instantiate(endlevelscreen);
                    instantiated = true;
                    Transform finalscore = popup.transform.GetChild(1);
                    TextMeshProUGUI scoretext = finalscore.gameObject.GetComponent<TextMeshProUGUI>();
                    scoretext.SetText("score: {0}", Score.CurrentScore);                    
                }
            
            }

        }*/
    }
}

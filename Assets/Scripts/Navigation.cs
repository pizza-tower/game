using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    int totalscenes;

    void Start()
    {
        totalscenes = SceneManager.sceneCountInBuildSettings;
        menu.SetActive(false);
    }
    public void GoBack() {
        int level = SceneManager.GetActiveScene().buildIndex;
        if (level > 0) {
            level--;
            menu.SetActive(false);
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
        if(level  + 1 < totalscenes) {
            level++;
            menu.SetActive(false);
            ResetVariables();
            GlobalData.level++;
            SceneManager.LoadScene(level);
        }
        else {
            menu.SetActive(true);
            
        }
    }

    void ResetVariables() {
        GlobalData.isFirstSlice = true;
        GlobalData.previousSlice="AnchorOne";
        GlobalData.isFirstFusionOver = false;
        GlobalData.gameover = false;
        Score.CurrentScore = 0;
    }

    public void RestartLevel() {
        int level = SceneManager.GetActiveScene().buildIndex;
        print("restart");
        ResetVariables();
        menu.SetActive(false);
        SceneManager.LoadScene(level);

    }


    // Update is called once per frame
    void Update()
    {
        // if(GlobalData.gameover){
        //     RestartSameLevel();
        // }else{
        if (GlobalData.gameover) {
            menu.SetActive(true);
        }
        if(Score.CurrentScore >= 30){
            StartNextLevel();
        }
    } 
    // }
}

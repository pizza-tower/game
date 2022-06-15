using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void StartNextLevel(){
        Score.CurrentScore = 0;
        if(GlobalData.level == 0){
            GlobalData.level = 1;
            SceneManager.LoadScene("Basic_Prototype 1");
        }else if(GlobalData.level == 1){
            SceneManager.LoadScene("GameOver");
        }else{
            Debug.Log("Wrong Place");
        }
    } 

    void RestartSameLevel(){
        Score.CurrentScore = 0;
        GlobalData.gameover = false;
        if(GlobalData.level == 0){
            SceneManager.LoadScene("Level0");
        }
        if(GlobalData.level == 1){
            SceneManager.LoadScene("Basic_Prototype 1");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalData.gameover){
            RestartSameLevel();
        }else{
            if(Score.CurrentScore >= 30){
                StartNextLevel();
            }
        } 
    }
}

// In the controller.cs file create an object of GameOver 
// Create a function GameOver(){GameOver.setup(points)}
// On the Unity Engine Drag and Drop the components in settings to connect different scripts. 

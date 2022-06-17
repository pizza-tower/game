using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    void Start()
    {
    }
    public void GoBack() {
        print(GlobalData.level);
        if (GlobalData.level > 0) {
            GlobalData.level--;
            menu.SetActive(false);
            SceneManager.LoadScene(GlobalData.level);

        }
    }

    void StartNextLevel() {
        if(GlobalData.level + 1 < SceneManager.sceneCount) {
            GlobalData.level++;
            SceneManager.LoadScene(GlobalData.level);
        }
        else {
            menu.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // if(GlobalData.gameover){
        //     RestartSameLevel();
        // }else{
        if(Score.CurrentScore >= 5){
            StartNextLevel();
        }
        } 
    // }
}

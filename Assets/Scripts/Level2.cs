using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Level2 : MonoBehaviour
{
    private bool isFlipActive = false;
    private Random rd;

    private int count = 0;

    //public string inputName;
    void Start()
    {
        StartCoroutine(CountDownTimerToFlip());
    }
    // Update is called once per frame
    void Update(){

        if (isFlipActive){
            // Debug.Log("Flip Active");
            GlobalData.KeyDown = true;
            GlobalData.KeyHold = true;
            GlobalData.KeyUp = false;
            // Debug.Log("Key Down" + GlobalData.KeyDown);
            // Debug.Log("Key Hold" + GlobalData.KeyHold);
            // Debug.Log("Key Up" + GlobalData.KeyUp);
        }else{
            GlobalData.KeyDown = Input.GetKeyDown(KeyCode.Space);
            GlobalData.KeyHold = Input.GetKey(KeyCode.Space);
            GlobalData.KeyUp = Input.GetKeyUp(KeyCode.Space);
            // Debug.Log("Key Down" + GlobalData.KeyDown);
            // Debug.Log("Key Hold" + GlobalData.KeyHold);
            // Debug.Log("Key Up" + GlobalData.KeyUp);
        }

        //control the flipper with space bar
        if (GlobalData.KeyDown && GlobalData.KeyHold && !GlobalData.KeyUp)
        {
            GetComponent<HingeJoint>().useMotor = true;
        }

        if (isFlipActive){
            isFlipActive = false;
            StartCoroutine(wait());
        }else{
            GlobalData.KeyUp = Input.GetKeyUp(KeyCode.Space);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<HingeJoint>().useMotor = false;
        }
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(1f);
        GetComponent<HingeJoint>().useMotor = false;
        GlobalData.countDownInProcess = false;
        StartCoroutine(CountDownTimerToFlip());
    }
    IEnumerator CountDownTimerToFlip(){
        if(!GlobalData.countDownInProcess){
            bool waiting = true;
            float timer = getTime(Score.CurrentScore);
            Debug.Log("CountDown and Score "+timer+" "+Score.CurrentScore);
            GlobalData.countDownInProcess = true;
            while (waiting){
                yield return new WaitForSeconds(0.1f);
                timer -= 1f;
                if (timer == 0){
                    waiting = false;
                }
            }
                isFlipActive = true;
        }
    }

    //Randomizing time value
    //Decreasing time period as the game progresses - measured by count value
    int getTime(int score){
        count++;
        int time = 0;
        switch (score){
            case int n when (n > 30):
                time = 20;
                break;
            case int n when (n > 25 && n <= 30):
                time = 25;
                break;
            case int n when (n >20 && n <= 25):
                time = 30;
                break;
            case int n when (n > 15 && n <= 20):
                time = 35;
                break;
            case int n when (n > 10 && n <= 15):
                time = 40;
                break;
            case int n when (n > 5 && n <= 10):
                time = 45;
                break;
            case int n when (n <= 5):
                time = 50;
                break;
        }
        return time;
    }
}
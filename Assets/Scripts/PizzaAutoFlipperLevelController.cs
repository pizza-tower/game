using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PizzaAutoFlipperLevelController : MonoBehaviour
{

    private bool isFlipActive = false;
    private int count = 0;
    private Random rd;

    //public string inputName;
    void Start()
    {
        rd = new Random();
        StartCoroutine(CountDownTimerToFlip());
    }

    // Update is called once per frame
    void Update()
    {

        if (isFlipActive)
        {
            Debug.Log("Flip Active");
            StartCoroutine(CountDownTimerToFlip());
            GlobalData.KeyDown = true;
            GlobalData.KeyHold = true;
            GlobalData.KeyUp = false;
        }
        else
        {
            GlobalData.KeyDown = Input.GetKeyDown(KeyCode.Space);
            GlobalData.KeyHold = Input.GetKey(KeyCode.Space);
            GlobalData.KeyUp = Input.GetKeyUp(KeyCode.Space);
        }

        //control the flipper with space bar
        if (GlobalData.KeyDown && GlobalData.KeyHold && !GlobalData.KeyUp)
        {
            GetComponent<HingeJoint>().useMotor = true;
        }
        if (isFlipActive)
        {
            isFlipActive = false;
            //GlobalData.KeyUp = true;
            StartCoroutine(CountDownTimerToFlip());
            StartCoroutine(wait());
        }
        else
        {
            GlobalData.KeyUp = Input.GetKeyUp(KeyCode.Space);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<HingeJoint>().useMotor = false;
        }
    }

    IEnumerator wait()
    {
        Debug.Log("Start wait");
        yield return new WaitForSeconds(1f);
        Debug.Log("End and set useMotor to false");
        GetComponent<HingeJoint>().useMotor = false;

    }
    IEnumerator CountDownTimerToFlip()
    {
        bool waiting = true;
        float timer = getTime(count);

        while (waiting)
        {
            yield return new WaitForSeconds(0.1f);
            timer -= 1f;
            if (timer == 0)
            {
                waiting = false;
                count++;
                Debug.Log("Count value: " + count);
                //this value is used to generate the time gap before the auto flip happens
            }
        }
        isFlipActive = true;
    }

    //Randomizing time value
    //Decreasing time period as the game progresses - measured by count value
    int getTime(int count)
    {
        int time = 0;
        switch (count)
        {
            case int n when (n >= 400):
                Console.WriteLine($"Count In 30 or above: {n}");
                time = rd.Next(40, 45);
                break;

            case int n when (n < 400 && n >= 250):
                time = rd.Next(40, 50);
                // Console.WriteLine($"Count In between 30 and 50: {n}");
                break;

            case int n when (n < 250 && n >= 200):
                time = rd.Next(50, 60);
                break;
            case int n when (n < 200 && n >= 100):
                time = rd.Next(60, 70);
                break;
            case int n when (n < 100 && n >= 75):
                time = rd.Next(70, 80);
                break;
            case int n when (n < 75 && n >= 40):
                time = rd.Next(80, 90);
                break;
            case int n when (n < 40 && n >= 10):
                time = rd.Next(90, 100);
                Console.WriteLine($"Count In between 5 and 3: {n}");
                break;
            case int n when (n < 10):
                time = rd.Next(100, 120);
                Console.WriteLine($"less than 3: {n}");
                break;
        }

        return time;
    }
}
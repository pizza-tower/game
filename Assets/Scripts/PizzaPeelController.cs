using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PizzaPeelController : MonoBehaviour
{
    //public string inputName;
    public float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
        //control the flipper with space bar
        if(KeyDown && KeyHold && !KeyUp)
        {
            GetComponent<HingeJoint>().useMotor = true;

            // Analytics tracking for time between thrown slices
            int timeElapsed = Mathf.RoundToInt(Time.time - startTime);
            Debug.Log(timeElapsed);
            startTime = Time.time;
            AnalyticsResult analyticsResult = Analytics.CustomEvent(
                "IdleTime",
                new Dictionary<string, object> {
                    { "Level", SceneManager.GetActiveScene().name },
                    { "Time", timeElapsed }
                }
            );
            Debug.Log("analyticsResult (IdleTime): " + analyticsResult);
            Analytics.FlushEvents();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<HingeJoint>().useMotor = false;
        }
        
       
    }
}
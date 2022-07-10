using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PizzaPeelController : MonoBehaviour
{
    //public string inputName;
    public float startTime;
    [SerializeField] [Range(0F, 10F)] float lerpTime;
    [SerializeField] Vector3[] myAngles;

    int angleIndex;
    int len;

    float t = 0f;
    void Start()
    {
        startTime = Time.time;
        len = myAngles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //automatically flip per second
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myAngles[angleIndex]), lerpTime*Time.deltaTime);

        t= Mathf.Lerp(t, 1f, lerpTime*Time.deltaTime);
        if(t>0.9f)
        {
            t = 0f;
            angleIndex += 1;
            angleIndex %= 2;
        }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PizzaPeelController : MonoBehaviour
{
    //public string inputName;
    public float startTime;

    [SerializeField] [Range(0f, 12f)] float lerpRotationTime;
    [SerializeField] Vector3[] myFlipAngles;
    [SerializeField] Vector3[] myThrowAngles;

    public int throwIt = 0;
    int flipAngleIndex; 
    int ThrowAngleIndex = 0;
    int lenFlip;
    float tFlip = 0f;
    float tThrow = 0f;

    int isThrowing = 0;
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

        if(isThrowing == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myThrowAngles[ThrowAngleIndex]), lerpRotationTime * Time.deltaTime);
            tThrow= Mathf.Lerp(tThrow, 1f, lerpRotationTime * Time.deltaTime);
            if(tThrow>0.9f)
            {
                tThrow = 0f;
                isThrowing = 0;
                flipAngleIndex = 0;
                tFlip = 0;
            }
            return;
        }
        if(KeyDown && KeyHold && !KeyUp)
        {
            isThrowing = 1;
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
        
        //normal flip animation, flip the peel to rotate the pizza slices
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myFlipAngles[flipAngleIndex]), lerpRotationTime * Time.deltaTime);
        tFlip= Mathf.Lerp(tFlip, 1f, lerpRotationTime * Time.deltaTime);
        if(tFlip>0.9f)
        {
            tFlip = 0f;
            flipAngleIndex += 1;
            flipAngleIndex %= 3;
        }
    
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalData : MonoBehaviour
{    

    public static int verticalFusionHeight=3;
    public static int level;
    public static bool isFirstSlice;
    public static string previousSlice;
    public static bool isFirstFusionOver;
    public static bool isFirstHorizontalFusionOver;
    public static bool gameover;
    public static int GoTransparent = 0;
    // Start is called before the first frame update
    void Start()
    {
       level = SceneManager.GetActiveScene().buildIndex;
        isFirstSlice = true;
        previousSlice="AnchorOne";
        isFirstFusionOver = false;
        isFirstHorizontalFusionOver = false;
        gameover = false;

    }

    // Update is called once per frame
    void Update()
    {


    }

    void setIsFirstSlice(bool status)
    {
        isFirstSlice = status;
    }

}

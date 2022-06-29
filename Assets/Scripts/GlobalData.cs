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
    public static bool isFirstFusionOver = false;
    public static bool isFirstHorizontalFusionOver;
    public static bool gameover;
    public static int GoTransparent = 0;
    public static List<List<GameObject>> globalList;
    public static int totalscenes;
    public static int nHorizontalFusions = 0;
    public static int nVerticalFusions = 0;

    // Start is called before the first frame update
    void Start()
    {        
        level = SceneManager.GetActiveScene().buildIndex;
        totalscenes = SceneManager.sceneCountInBuildSettings;
        isFirstSlice = true;
        previousSlice="AnchorOne";
        isFirstFusionOver = false;
        isFirstHorizontalFusionOver = false;
        gameover = false;
        globalList = new List<List<GameObject>>();
        for(int i = 0; i < 6; i++)
        {
            globalList.Add(new List<GameObject>());
        }
        Debug.Log("get global list count" + globalList.Count);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void setIsFirstSlice(bool status)
    {
        isFirstSlice = status;
    }

    public static void ResetGlobalList(){
        globalList = new List<List<GameObject>>();
        for(int i = 0; i < 6; i++)
        {
            globalList.Add(new List<GameObject>());
        }
    }

}

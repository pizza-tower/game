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
    public static int LevelRewardConsume = 0;

    public static Dictionary<string, int> MaxSlices;
    public static Dictionary<string, List<SliceColor>> ValidSlices;
    public static Dictionary<string, List<List<SliceColor>>> ValidCombinations;
    // Start is called before the first frame update
    void Start()
    {        
        level = SceneManager.GetActiveScene().buildIndex;
        totalscenes = SceneManager.sceneCountInBuildSettings;
        print(totalscenes);
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

        //Setup number of slices per level
        MaxSlices = new();
        MaxSlices.Add("Level1", 60);

        //Setup valid slices that can be spawned for a level
        ValidSlices = new();
        ValidSlices.Add("Level1", new() { SliceColor.Red, SliceColor.Yellow });

        //Setup valid combinations in clickwise order
        SliceColor r = SliceColor.Red;
        SliceColor y = SliceColor.Yellow;
        ValidCombinations = new();
        ValidCombinations.Add("Level1", new() {
            new() { r, r, r, r, r, r },
            new() { y, y, y, y, y, y },
            new() { r, r, r, y, y ,y }
        });
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

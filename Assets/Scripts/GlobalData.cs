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
        MaxSlices.Add("Level0", 60);
        MaxSlices.Add("Level1", 5);
        MaxSlices.Add("Level2", 60);
        MaxSlices.Add("Level3", 60);
        MaxSlices.Add("Level4", 60);
        MaxSlices.Add("Level5", 60);

        //Setup valid slices that can be spawned for a level
        ValidSlices = new();
        // TutorialHandler will change this value in Runtime for walkthrough purposes.
        ValidSlices.Add("Level0", new() { SliceColor.Red, SliceColor.Yellow });
        ValidSlices.Add("Level1", new() { SliceColor.Red, SliceColor.Yellow });
        ValidSlices.Add("Level2", new() { SliceColor.Red, SliceColor.Yellow });
        ValidSlices.Add("Level3", new() { SliceColor.Red, SliceColor.Yellow, SliceColor.Blue });
        ValidSlices.Add("Level4", new() { SliceColor.Red, SliceColor.Yellow, SliceColor.Blue });
        ValidSlices.Add("Level5", new() { SliceColor.Red, SliceColor.Yellow, SliceColor.Blue, SliceColor.DarkBrown, SliceColor.Green });
        ValidSlices.Add("Level6", new() { SliceColor.Red, SliceColor.Yellow, SliceColor.Blue, SliceColor.DarkBrown, SliceColor.Green });

        //Setup valid combinations in clickwise order
        SliceColor r = SliceColor.Red;
        SliceColor y = SliceColor.Yellow;
        SliceColor d = SliceColor.DarkBrown;
        SliceColor g = SliceColor.Green;
        SliceColor b = SliceColor.Blue;
        ValidCombinations = new();
        //Level 0
        ValidCombinations.Add("Level0", new() {
            new() { r, r, r, r, r, r },
            new() { r, r, r, y, y, y },
            new() { y, y, y, y, y, y }, 
        });
        //LEVEL1
        ValidCombinations.Add("Level1", new() {
            new() { r, r, r, y, y ,y }
        });
        //LEVEL2
        ValidCombinations.Add("Level2", new() {
            new() { r, r, y, y, y, y },
            new() { y, y, y, y, y, y },
            new() { r, r, r, r, r, r }
        });
        //LEVEL3
        ValidCombinations.Add("Level3", new() {
            new() { r, r, r, y, y, y },
            new() { r, y, y, y, y, y },
            new() { y, y, y, y, y, y }
        });
        //LEVEL4
         ValidCombinations.Add("Level4", new() {
            new() { b, y, r, r, r, r },
            new() { y, r, y, y, y, b },
            new() { r, y, r, y, r, y }
        });
        //LEVEL5
         ValidCombinations.Add("Level5", new() {
            new() { r, r, r, r, y, y },
            new() { y, y, y, y, y, y },
            new() { r, r, r, y, y, y }
        });
        //LEVEL6
         ValidCombinations.Add("Level6", new() {
            new() { r, r, r, g, y, y },
            new() { y, y, y, y, r, d },
            new() { r, r, r, y, y, y }
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

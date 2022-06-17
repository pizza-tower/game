using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalData : MonoBehaviour
{    

    public static int verticalFusionHeight=3;
    public static int level;
    public static bool isFirstSlice = true;
    public static string previousSlice="AnchorOne";
    public static bool isFirstFusionOver = false;

    // Start is called before the first frame update
    void Start()
    {
       level = SceneManager.GetActiveScene().buildIndex;
       print("level from here ");
       print(level);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{    

    public static int verticalFusionHeight=3;
    public static int level = 0;
    public static bool isFirstSlice = true;
    public static string previousSlice="AnchorOne";
    public static bool isFirstFusionOver = false;

    // Start is called before the first frame update
    void Start()
    {
       
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

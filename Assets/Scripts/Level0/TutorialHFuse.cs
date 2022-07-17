using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

class TutorialHFuse : MonoBehaviour
{
    public static List<KeyValuePair<string, GameObject>> sliceWithCoords = new List<KeyValuePair<string, GameObject>>();
    public static Vector3 position;
    
    public static List<SliceColor> sColors = new List<SliceColor>(){
        SliceColor.Red,
        SliceColor.Red,
        SliceColor.Yellow,
        SliceColor.Yellow,
        SliceColor.Yellow
    };

    public static Dictionary<string, Vector3> sliceRotation = new Dictionary<string, Vector3>();
    public static void InitializeVars()
    {

        sliceRotation.Add("col1_slice1", new Vector3(0f,0f,0f));
        sliceRotation.Add("col2_slice1", new Vector3(0f,60.0000038f,0f));
        sliceRotation.Add("col3_slice1", new Vector3(0f,120.000008f,0f) );
        sliceRotation.Add("col4_slice1", new Vector3(0f,180f,0f));
        sliceRotation.Add("col5_slice1", new Vector3(0f,240f,0f));
        sliceRotation.Add("col6_slice1", new Vector3(0f,300f,0f));

        position = new Vector3(4.36000013f,-1.04999995f,-10.8999996f);
        // position = new Vector3(4.19999981f,-1.10902405f,-11.6400003f);
        
// /Vector3(4.36000013,-1.71000004,-10.8999996)
    }

    static void createSlice(int i, GameObject slice)
    {
        GameObject NewSlice = Instantiate(slice) as GameObject;
        NewSlice.tag = "hardcoded";
        NewSlice.name = "col" + i + "_slice1";
        NewSlice.transform.position = position;
        NewSlice.transform.rotation = Quaternion.Euler(sliceRotation["col" + i + "_slice1"]);

        NewSlice.GetComponent<PizzaRotation>().mColor = sColors[i - 1];
        NewSlice.GetComponent<PizzaRotation>().hardcoded = true;
        NewSlice.GetComponent<PizzaParabola>().enabled = false;

        GlobalData.globalList[i - 1].Add(GameObject.Find("col" + i + "_slice1"));
    }

    public static void placeSlices(GameObject slice){

        for(int i=1;i<=5;i++){
            //spawn a new slice at spawner
            createSlice(i, slice);
        }
    }

    public static void checkGlobalList(GameObject slice) {
        for (int i = 1;i <= 5; i++)
        {
            if(GlobalData.globalList[i-1].Count == 0)
            {
                createSlice(i, slice);
            }
        }
    }
}
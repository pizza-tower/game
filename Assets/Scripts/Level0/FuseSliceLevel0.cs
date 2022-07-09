using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class FuseSliceLevel0 : MonoBehaviour
{
    public GameObject ui_handler;
    // Start is called before the first frame update
    void Start()
    {
        ui_handler = GameObject.Find("UIHandler");
    }

    void Update(){
        
    }

    private static bool mCheckTopNSlices(List<GameObject> SList)
    {
        int n = GlobalData.verticalFusionHeight;

        //Debug.Log("Inside check slices");
        //if the height of tower is shorter than n, return false
        if (n > SList.Count || SList.Count == 0)
        {
            return false;
        }

        //if any slice of the top 'n' slices is not of desired color, return false
        //else return true

         var color =
            SList[SList.Count - 1].GetComponent<PizzaRotationLevel0>().IsRed;
        for (int i = 1; i <= n; i++)
        {
            //There exists a slice in top n slices that is not of desired color
            if (
                SList[SList.Count - i].GetComponent<PizzaRotationLevel0>().IsRed !=color
            ) return false;

            //Brown slice cannot be fused with other slices
            if (SList[SList.Count - i].GetComponent<PizzaRotationLevel0>().IsBrown == 1)
             return false;
        }

        //All top n slices are of desired color
        return true;
    }

    public static void mVertFuse(List<GameObject> SList)
    {
        int n = GlobalData.verticalFusionHeight;

        //Check if top n slices in have same color as the latest slice
        if (mCheckTopNSlices(SList))
        {
            //Debug.Log("Slices were same colored");
            for (int k = 1; k <= n; k++)
            {
                Destroy(SList[SList.Count - k]);
                GlobalData.nVerticalFusions++;
                AnalyticsResult vertFusionAnalytics = Analytics.CustomEvent("VerticalFusions", new Dictionary<string, object>{{"Level", SceneManager.GetActiveScene().name}, {"VerticalFusions", GlobalData.nVerticalFusions}});


            }
            // if(GlobalData.isFirstFusionOver==false)
            // {
            //     Debug.Log("This is the first fusion");
            //     GameObject ui_handler = GameObject.Find("UIHandler");
            //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Well done! You fused 3 slices and made it disappear!"));
            // }
            // GlobalData.isFirstFusionOver = true;
            SList.RemoveRange(SList.Count - n, n);
            GlobalData.isFirstFusionOver = true;
            //No score for vertical fusion, hence commenting it out.
            //Score.EarnScore();
            //Debug.Log("we earn score +" + Score.CurrentScore);
        }
    }

 /*Bomb will fuse all slice in that SList
    */
    public static void BombFuse(List<GameObject> SList)
    {
        int n = SList.Count;
        for (int k = 0; k < SList.Count; k++)
        {
            Destroy(SList[k]);
        }
        SList.RemoveRange(0, n);
        //Destroy(GameObject.FindWithTag("0"));
        Debug.Log("boom");
    }
    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }

    /* Logic for horizontal fusion:
    Author: Manasi
    Use a global list which contains all 6 (vertical) lists.
    Find the minimum height from all the lists since the pizza could be only formed at that level and not above.
    Assumption: If checking for pizza at ith level then it means that all the i-1 levels have been already checked.
    
    Once the minimum height is found, iterate over all lists and check the color of slices at that height
    if same, then destroy the slices and shift all the slices down by one level which are above the min height.

    */
    public static void mHorizontalFuse()
    {
        
    }
}

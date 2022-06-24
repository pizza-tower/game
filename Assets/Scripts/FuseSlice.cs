using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FuseSlice : MonoBehaviour
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
            SList[SList.Count - 1].GetComponent<PizzaRotation>().IsRed;
        for (int i = 1; i <= n; i++)
        {
            //There exists a slice in top n slices that is not of desired color
            if (
                SList[SList.Count - i].GetComponent<PizzaRotation>().IsRed !=color
            ) return false;
        }

        //All top n slices are of desired color
        return true;
    }

    public static void mVertFuse(List<GameObject> SList, int Level)
    {
        int n = GlobalData.verticalFusionHeight;

        //Check if top n slices in have same color as the latest slice
        if (mCheckTopNSlices(SList))
        {
            Debug.Log("Slices were same colored");
            for (int k = 1; k <= n; k++)
            {
                Destroy(SList[SList.Count - k]);
            }
            if(GlobalData.isFirstFusionOver==false && Level==0)
            {
                Debug.Log("This is the first fusion");
                GameObject ui_handler = GameObject.Find("UIHandler");
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Well done! You fused 3 slices and scored! Good luck!"));
                
            }
            GlobalData.isFirstFusionOver = true;
            SList.RemoveRange(SList.Count - n, n);

            //No score for vertical fusion, hence commenting it out.
            //Score.EarnScore();
            //Debug.Log("we earn score +" + Score.CurrentScore);
        }
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
    public static void mHorizontalFuse(int Level)
    {
        List<List<GameObject>> allLists = SliceList.globalList;
        //int minHeight = SliceList.globalList.Min(y=>y.Count);
        int minHeight = 9999;

        //Had to delete extra slicelists attached to anchors 2 to 6 to get the correct count
        //List<List<GameObject>> allLists = SliceList.globalList;

        foreach (List<GameObject> anchorList in allLists)
        {
            if (anchorList.Count < minHeight)
            {
                minHeight = anchorList.Count;
            }
        }

        //found the minimum tower height
        //for now check if all the slices at that height are same, if yes, fuse and disappear
        bool sameColor = false;
        bool halfPizza = false;
        if (minHeight != 0)
        {
            if (
                allLists[0].Count >= minHeight && 
                allLists[1].Count >= minHeight &&
                allLists[2].Count >= minHeight &&
                allLists[3].Count >= minHeight &&
                allLists[4].Count >= minHeight &&
                allLists[5].Count >= minHeight
            )
            {

                var givenColor =
                    allLists[0][minHeight - 1]
                        .GetComponent<PizzaRotation>().IsRed;
                for (int i = 1; i < 6; i++)
                {
                    if (
                        givenColor ==
                        allLists[i][minHeight - 1]
                            .GetComponent<PizzaRotation>().IsRed
                    )
                    {
                        sameColor = true;
                    }
                    else
                    {
                        sameColor = false;

                        break;
                    }
                }

                /* Adding logic to detect if a half & half pizza is made
                3 adjacents slices to be of same color. Possible combinations
                are handled in the if else conditions.
                TODO: since the colors are being stored in a vairable, it is now 
                possible to remove the upper if condition and directly check if all the colors
                are the same.
                */
                
                var color1 = allLists[0][minHeight - 1].GetComponent<PizzaRotation>().IsRed;
                var color2 = allLists[1][minHeight - 1].GetComponent<PizzaRotation>().IsRed;
                var color3 = allLists[2][minHeight - 1].GetComponent<PizzaRotation>().IsRed;
                var color4 = allLists[3][minHeight - 1].GetComponent<PizzaRotation>().IsRed;
                var color5 = allLists[4][minHeight - 1].GetComponent<PizzaRotation>().IsRed;
                var color6 = allLists[5][minHeight - 1].GetComponent<PizzaRotation>().IsRed;

                if(color1 == color2 && color2 == color3 && color4==color5 && color5 == color6 && color1!=color4){
                    halfPizza= true;
                }else if(color2 == color3 && color3 == color4 && color5==color6 && color6 == color1 && color2!=color5){
                    halfPizza = true;
                }else if(color3 == color4 && color4 == color5 && color6==color1 && color1 == color2 && color3!=color6){
                    halfPizza = true;}
            }
        }

    if (sameColor || halfPizza)
        {
            foreach (List<GameObject> anchorList in allLists)
            {
                Destroy(anchorList[minHeight - 1]);
                if(anchorList.Count>=minHeight){
                    

                    for (int i = minHeight; i < anchorList.Count; i++){
                        Vector3 slicePosition = anchorList[i].transform.position;
                        slicePosition.y = slicePosition.y - 0.18f;
                        //var newEndPoint = anchorList[minHeight].transform.position.y - 0.15;
                        //Vector3 newVector = new Vector3(oldPosition.x, oldPosition.y - 15f, oldPosition.z);
                        //anchorList[minHeight].transform.TransformPoint(newVector); //= Vector3.MoveTowards(oldPosition, newVector, Time.deltaTime * 1);
                        anchorList[i].transform.position = slicePosition;
                    }   
                }
                anchorList.RemoveAt(minHeight - 1);

            }
            Score.EarnScore();
        }
    }
}

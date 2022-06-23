using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pod3 : MonoBehaviour
{

    public static AudioSource explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        explosionAudio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


//This function checks the heights of all 6 stacks
//destroys top 3 or all (whichever is higher) slices from top of the tallest stack
 public static void mDropBomb()
    {
        List<List<GameObject>> allLists = SliceList.globalList;
        //int minHeight = SliceList.globalList.Min(y=>y.Count);
        int maxHeight = 0;
        int index=-1;
        //Had to delete extra slicelists attached to anchors 2 to 6 to get the correct count
        //List<List<GameObject>> allLists = SliceList.globalList;

        //foreach (List<GameObject> anchorList in allLists)
        for(int i=0;i<allLists.Count;i++)
        {
            if (allLists[i].Count >= maxHeight)
            {
                maxHeight = allLists[i].Count;
                index=i;
                //Find the index of stack with tallest height and destroy top 3 slices
            }
        }
        if(index!=-1 && maxHeight>0)
        {
             for (int k = 1; k <= 3; k++)
            {
                if(allLists[index].Count - k >= 0)
                {
                    Destroy(allLists[index][allLists[index].Count - k]);
                }
            }

        explosionAudio.Play();

        int destroys=0;
        while(allLists[index].Count - 1 >= 0 && destroys<3){
            allLists[index].RemoveRange(allLists[index].Count - 1,1);
            destroys++;
        }

        }

        //allLists[index].RemoveRange(SList.Count - n, n);
        Debug.Log($"Max height {maxHeight}");
    }

}

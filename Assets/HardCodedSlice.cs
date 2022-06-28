using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCodedSlice : MonoBehaviour
{    
    private GameObject AnchorToFind;
    private string ListToAdd = "AnchorOne";
    private SliceList List;
    // Start is called before the first frame update
    void Start()
    {       
        // if (gameObject.tag == "R_1" ||gameObject.tag == "Y_1" || gameObject.tag == "B_1")
        // {
        //     AnchorToFind = GameObject.FindWithTag("AnchorOne");
        //     List = AnchorToFind.GetComponent<SliceList>();
        //     List.SList.Add(gameObject);            
        // }
        // if(gameObject.tag == "R_2" ||gameObject.tag == "Y_2" || gameObject.tag == "B_2")
        // {          
        //     AnchorToFind = GameObject.FindWithTag("AnchorTwo");
        //     List = AnchorToFind.GetComponent<SliceList>();
        //     List.SList.Add(gameObject);            
        // }
    
        //Fixed hardcoded stacks for now. Need to randomize them later.
        GlobalData.globalList[3].Add(GameObject.Find("stack1_slice1"));
        GlobalData.globalList[3].Add(GameObject.Find("stack1_slice2"));
        GlobalData.globalList[3].Add(GameObject.Find("stack1_slice3"));
        GlobalData.globalList[3].Add(GameObject.Find("stack1_slice4"));
        GlobalData.globalList[3].Add(GameObject.Find("stack1_slice5"));

        GlobalData.globalList[4].Add(GameObject.Find("stack2_slice1"));
        GlobalData.globalList[4].Add(GameObject.Find("stack2_slice2"));
        GlobalData.globalList[4].Add(GameObject.Find("stack2_slice3"));
        GlobalData.globalList[4].Add(GameObject.Find("stack2_slice4"));
        GlobalData.globalList[4].Add(GameObject.Find("stack2_slice5"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

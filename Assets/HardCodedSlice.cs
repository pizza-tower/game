using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCodedSlice : MonoBehaviour
{    
    private GameObject AnchorToFind;
    private string ListToAdd = "AnchorOne";
    private SliceList List;
    public int Level;
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
        if(Level==2){

        GlobalData.globalList[0].Add(GameObject.Find("col1_slice1"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice2"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice3"));

        GlobalData.globalList[1].Add(GameObject.Find("col2_slice1"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice2"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice3"));

        GlobalData.globalList[2].Add(GameObject.Find("col3_slice1"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice2"));
        
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice1"));

        GlobalData.globalList[4].Add(GameObject.Find("col5_slice1"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice2"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice3"));   

        GlobalData.globalList[5].Add(GameObject.Find("col6_slice1"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice2"));    
   
        }
        else if(Level==3){

        GlobalData.globalList[0].Add(GameObject.Find("col1_slice1"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice2"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice3"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice4"));

        GlobalData.globalList[1].Add(GameObject.Find("col2_slice1"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice2"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice3"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice4"));

        GlobalData.globalList[2].Add(GameObject.Find("col3_slice1"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice2"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice3"));

        GlobalData.globalList[3].Add(GameObject.Find("col4_slice1"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice2"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice3"));

        GlobalData.globalList[4].Add(GameObject.Find("col5_slice1"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice2"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice3"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice4"));

        GlobalData.globalList[5].Add(GameObject.Find("col6_slice1"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice2"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice3"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice4"));
     
        }
        else if(Level==4){
            
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice1"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice2"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice3"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice4"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice5"));

        GlobalData.globalList[1].Add(GameObject.Find("col2_slice1"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice2"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice3"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice4"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice5"));

        GlobalData.globalList[2].Add(GameObject.Find("col3_slice1"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice2"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice3"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice4"));

        GlobalData.globalList[3].Add(GameObject.Find("col4_slice1"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice2"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice3"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice4"));

        GlobalData.globalList[4].Add(GameObject.Find("col5_slice1"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice2"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice3"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice4"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice5"));

        GlobalData.globalList[5].Add(GameObject.Find("col6_slice1"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice2"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice3"));        
     
        }
        else if(Level==5){
     
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice1"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice2"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice3"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice4"));
        GlobalData.globalList[0].Add(GameObject.Find("col1_slice5"));

        GlobalData.globalList[1].Add(GameObject.Find("col2_slice1"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice2"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice3"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice4"));
        GlobalData.globalList[1].Add(GameObject.Find("col2_slice5"));

        GlobalData.globalList[2].Add(GameObject.Find("col3_slice1"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice2"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice3"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice4"));
        GlobalData.globalList[2].Add(GameObject.Find("col3_slice5"));

        GlobalData.globalList[3].Add(GameObject.Find("col4_slice1"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice2"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice3"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice4"));
        GlobalData.globalList[3].Add(GameObject.Find("col4_slice5"));

        GlobalData.globalList[4].Add(GameObject.Find("col5_slice1"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice2"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice3"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice4"));
        GlobalData.globalList[4].Add(GameObject.Find("col5_slice5"));

        GlobalData.globalList[5].Add(GameObject.Find("col6_slice1"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice2"));
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice3"));    
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice4"));    
        GlobalData.globalList[5].Add(GameObject.Find("col6_slice5"));    

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

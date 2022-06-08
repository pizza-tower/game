    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Fuse : MonoBehaviour
    {  
        // Start is called before the first frame update
        void Start()
        {
    
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private bool mCheckTopNSlices(int direction, string color="y"){
             int n = GlobalData.verticalFusionHeight;
                //if the height of tower is shorter than n, return false
                if(n > GlobalData.columnSliceList[direction].Count){
                    return false;
                }
            //if any slice of the top n slices is not of desired color, return false
            //else return true
            int ColumnSliceCount = GlobalData.columnSliceList[direction].Count;
            for(int i=1;i<=n;i++){
                //There exists a slice in top n slices that is not of desired color
                if(GlobalData.columnSliceList[direction][GlobalData.columnSliceList[direction].Count-i]!=color)
                    return false;
            }
            //All top n slices are of desired color
            return true;
        }

        private void OnCollisionEnter(Collision collsion)
        {
            int n = GlobalData.verticalFusionHeight;
            //Fetch direction of the incoming new slice
            int direction = 0;       
            // When the incoming new slice touches an existing same colored slice, 
            // destroy the existing slice present on the top of the tower
            if (collsion.gameObject.GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
                {                   
                    //Check if top n slices in have same color as the incoming slice
                    if(mCheckTopNSlices(direction) && collsion.gameObject.tag.ToString().Contains("NewSlice")){     
                    //Maintains the number of vertical fusions done.                          
                    GlobalData.numSlicesFused++;                  
                        Destroy(gameObject);   
                    }                
                }            
            else{
                GlobalData.numSlicesFused=0;
            }  
            //When N vertical fusions are completed, We also destroy the incoming itself,
            //Reset the count of fusions done to zero, and update the tower height
            if(GlobalData.numSlicesFused == n){
                Destroy(collsion.gameObject);
                GlobalData.numSlicesFused=0;
                GlobalData.columnSliceList[direction].RemoveRange(GlobalData.columnSliceList[direction].Count - n, n);     
            }
            
        }

        

    }

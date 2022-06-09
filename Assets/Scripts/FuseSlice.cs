        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class FuseSlice : MonoBehaviour
        {  
            // Start is called before the first frame update
            void Start()
            {
        
            }

            // Update is called once per frame
            void Update()
            {
                            
            }


            private static bool mCheckTopNSlices(List<GameObject> SList){
                int n = GlobalData.verticalFusionHeight;
                //Debug.Log("Inside check slices");
                    //if the height of tower is shorter than n, return false
                    if(n > SList.Count || SList.Count==0){
                        return false;
                    }
                //if any slice of the top 'n' slices is not of desired color, return false
                //else return true           
                var color=SList[SList.Count-1].GetComponent<MeshRenderer>().material.color;
                for(int i=1;i<=n;i++){
                    //There exists a slice in top n slices that is not of desired color
                if(SList[SList.Count-i].GetComponent<MeshRenderer>().material.color!=color)
                        return false;                                   
                }
                //All top n slices are of desired color
                return true;
            }

        
            public static void mVertFuse(List<GameObject> SList){
              
                int n = GlobalData.verticalFusionHeight;       
                        //Check if top n slices in have same color as the latest slice
                        if(mCheckTopNSlices(SList)){   
                            Debug.Log("Slices were same colored");  
                            for(int k=1;k<=n;k++){
                                Destroy(SList[SList.Count-k]);   
                            }                
                            SList.RemoveRange(SList.Count-n,n);
                        }                         
            }
    
        }

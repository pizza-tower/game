using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FuseSlice : MonoBehaviour
{
    int ID;
    int collisions;
    public GameObject mergedSlice;
    int countMergedSlices=0;
    // Start is called before the first frame update
    void Start()
    {
        ID = GetInstanceID();
        collisions=0;
    }

    // private void OnCollisionEnter2D(Collision2D collsion)
    // {            
    //     if (collsion.gameObject.CompareTag("NewSlice"))
    //     {       
    //         GameObject o=null;     
    //         int count=0;
    //         if (collsion.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
    //         {
    //             while(count<3){
    //             if (ID < collsion.gameObject.GetComponent<FuseSlice>().ID) { return; }
    //             Debug.Log($"SENDING MESSAGE FROM {gameObject.name}");                
    //             o = Instantiate(mergedSlice, transform.position, Quaternion.identity) as GameObject;
    //             Destroy(collsion.gameObject);
    //             Destroy(gameObject);     
    //             count++;
    //             if(count!=2)
    //             Destroy(o);
    //             }                          
    //         }
            
    //     }
    // }


private void OnCollisionEnter2D(Collision2D collsion)
    {       
        
        //if(collsion.gameObject.GetComponent<FuseSlice>().collisions>2)
        //{return;}
        //collsion.gameObject.GetComponent<FuseSlice>().collisions++;

        //if (collsion.gameObject.CompareTag("NewSlice"))
        //{       
            GameObject o=null;     
            int count=0;



            if (collsion.gameObject.GetComponent<SpriteRenderer>().color== GetComponent<SpriteRenderer>().color)
            {
            if (collsion.gameObject.GetComponent<SpriteRenderer>().tag.ToString().Contains("NewSlice") && GetComponent<SpriteRenderer>().tag.ToString().Contains("NewSlice"))
            {             
                if (ID < collsion.gameObject.GetComponent<FuseSlice>().ID) { return; }
                Debug.Log($"SENDING MESSAGE FROM {gameObject.name}");                 
                o= Instantiate(mergedSlice, transform.position, Quaternion.identity) as GameObject;
                //o.tag="hi"; 
                o.tag="MergedSlice";                                    
                Destroy(collsion.gameObject);
                Destroy(gameObject);                                                                                             
            }       
             else if (collsion.gameObject.GetComponent<SpriteRenderer>().tag.ToString().Contains("MergedSlice") || GetComponent<SpriteRenderer>().tag.ToString().Contains("MergedSlice"))
            {             
                //if (ID < collsion.gameObject.GetComponent<FuseSlice>().ID) { return; }
                Debug.Log($"SENDING MESSAGE FROM mergedslice {gameObject.name}");                 
                o= Instantiate(mergedSlice, transform.position, Quaternion.identity) as GameObject;
                o.tag="FullMergedSlice";                                     
                Destroy(collsion.gameObject);
                Destroy(gameObject);                                                                                             
            }  
             else if (collsion.gameObject.GetComponent<SpriteRenderer>().tag.ToString().Contains("FullMergedSlice") || GetComponent<SpriteRenderer>().tag.ToString().Contains("FullMergedSlice"))
             {
                  Debug.Log($"SENDING MESSAGE FROM fullmergedslice {gameObject.name}");   
                 return;
             }
    }

        //}
    }


    // Update is called once per frame
    void Update()
    {
        
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    int ID;
      public GameObject mergedSlice;

    // Start is called before the first frame update
    void Start()
    {
        ID=GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collsion)
    {

        GameObject o=null;  
         if (collsion.gameObject.GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
            {       
         if (collsion.gameObject.GetComponent<MeshRenderer>().tag.ToString().Contains("NewSlice") && GetComponent<MeshRenderer>().tag.ToString().Contains("NewSlice"))
         {
             if (ID < collsion.gameObject.GetComponent<Fuse>().ID) { return; }           
             o= Instantiate(mergedSlice, transform.position, Quaternion.identity) as GameObject;
                //o.tag="hi"; 
                o.tag="MergedSlice";                                    
                Destroy(collsion.gameObject);
                Destroy(gameObject);   
               // Destroy(o);                                                            
        }
           else if (collsion.gameObject.GetComponent<MeshRenderer>().tag.ToString().Contains("MergedSlice") || GetComponent<MeshRenderer>().tag.ToString().Contains("MergedSlice"))
            {             
                //if (ID < collsion.gameObject.GetComponent<FuseSlice>().ID) { return; }
                Debug.Log($"SENDING MESSAGE FROM mergedslice {gameObject.name}");                 
                o= Instantiate(mergedSlice, transform.position, Quaternion.identity) as GameObject;
                o.tag="FullMergedSlice";                                     
                Destroy(collsion.gameObject);
                Destroy(gameObject);   
                //Destroy(o);                                                                                             
            }  
             else if (collsion.gameObject.GetComponent<MeshRenderer>().tag.ToString().Contains("FullMergedSlice") || GetComponent<MeshRenderer>().tag.ToString().Contains("FullMergedSlice"))
             {
                  Debug.Log($"SENDING MESSAGE FROM fullmergedslice {gameObject.name}");  
                  //Destroy(o); 
                 return;
             }
    }
    }
}

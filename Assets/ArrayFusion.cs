using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayFusion : MonoBehaviour
{
    
    // Start is called before the first frame update
    Transform Block1;
    Transform Block2;
    bool CanMerge;
        public float MergeSpeed;
            public float Distance;

    void Start()
    {
        if(GlobalData.col1.Count<5){

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
         GameObject[] cubes=GameObject.FindGameObjectsWithTag("MergeBlock");
        Debug.Log($"No. of cubes{cubes.Length}");
        for(int i=0;i<cubes.Length;i++){ 
            GlobalData.col1.Add(cubes[i]);
        }
        Debug.Log($"No. of cubes in AL{GlobalData.col1.Count}");
       
        GameObject curr=GlobalData.col1[GlobalData.col1.Count-1];
        Debug.Log(curr.GetComponent<MeshRenderer>().material.color);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



      private void FixedUpdate()
    {
        MoveTowards();
    }

    public void MoveTowards()
    
     {
        //Debug.Log("here");
        if (CanMerge)
        {
            Debug.Log("inside");
            transform.position = Vector3.MoveTowards(Block1.position, Block2.position, MergeSpeed*Time.deltaTime);

            Debug.Log($"Block1 Pos {Block1.position}");
            Debug.Log($"Block2 Pos {Block2.position}");
            Debug.Log($"Vector3 Dist {Vector3.Distance (Block1.position, Block2.position)}");
            Debug.Log($"Distance {Distance}");
            if (Vector3.Distance(Block1.position, Block2.position) < Distance)
            {                
                Debug.Log("About to destroy");
                //GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                Destroy(Block1.gameObject);
                Destroy(Block2.gameObject);
                //Destroy(gameObject);
                for(int i = 0; i < GlobalData.col1.Count; i++)
                {
                    Destroy(GlobalData.col1[i]);            
                }
                Destroy(gameObject);
            }
        }
    }

private void OnCollisionExit(Collision collision){
    Debug.Log("Finished");
}

      private void OnCollisionEnter(Collision collision)
    {
        //UpdateColumnArray();
        //check if upper 3 are of same color
        if(true){


                Block1 = transform;
                Block2 = collision.transform;
                  if (collision.gameObject.CompareTag("MergeBlock"))
        {
 if(collision.gameObject.GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
            {
            CanMerge=true;
            }
        }
            
        //     Destroy(GlobalData.col1[GlobalData.col1.Count-1]);
        //     Destroy(GlobalData.col1[GlobalData.col1.Count-2]);
        //     Destroy(GlobalData.col1[GlobalData.col1.Count-3]);            
        
        //bool fuse=GlobalData.bFuse(1);

    }
}
}

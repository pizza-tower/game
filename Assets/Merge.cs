using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    public GameObject MergedObject;
    
    Transform Block1;
    Transform Block2;
    public float Distance;
    public float MergeSpeed;
    bool CanMerge;
    // Start is called before the first frame update
    void Start()
    {
        ID = GetInstanceID();   
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
            transform.position = Vector3.MoveTowards(Block1.position, Block2.position, MergeSpeed);
            Debug.Log("inside1");
            if (Vector3.Distance(Block1.position, Block2.position) < Distance)
            {                
               Debug.Log("inside2");
                //GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity) as GameObject;
                Destroy(Block1.gameObject);
                Destroy(Block2.gameObject);
                //Destroy(gameObject);
                for(int i = 0; i < DataRecorder.destroyItems.Length; i++)
                {
                    Destroy(DataRecorder.destroyItems[i]);
                }
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("yo");
        if (collision.gameObject.CompareTag("MergeBlock"))
        {
            if(collision.gameObject.GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
            {
                Block1 = transform;
                Block2 = collision.transform;
                DataRecorder.collisionTime++;
                
                Debug.Log($"SENDING MESSAGE FROM {gameObject.name} With numofBlocks {DataRecorder.numOfBlocks}");
                Debug.Log($"SENDING MESSAGE FROM {gameObject.name} With collision time {DataRecorder.collisionTime}");
                DataRecorder.destroyItems[DataRecorder.numOfBlocks] = collision.gameObject;
                    
                DataRecorder.numOfBlocks++;
                    
                 
                Debug.Log($"Collision Time:{DataRecorder.collisionTime}");
              
                if (DataRecorder.collisionTime == 6)
                {
                    Debug.Log("Running.。。。。");
                     Debug.Log("Reached 6");
                    DataRecorder.collisionTime = 0;
                    DataRecorder.numOfBlocks = 0;
                      
                    //Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<Rigidbody>());
                    CanMerge = true;  
                }
            }
            else
            {
                DataRecorder.collisionTime = 0;
            }
        }
    }
}
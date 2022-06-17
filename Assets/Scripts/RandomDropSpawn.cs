using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDropSpawn : MonoBehaviour
{
    
    public GameObject YellowPrefab;
    public GameObject RedPrefab;
    public int NeedsNewSlice = 1;
    public int NewSliceSpawnSeconds;
    public int IsRed = 0;
    string[] RedTags = new string[] { "R_1", "R_2", "R_3", "R_4", "R_5", "R_6" };
    string[] YellowTags = new string[] { "Y_1", "Y_2", "Y_3", "Y_4", "Y_5", "Y_6" };
    string[] Anchors = new string[] { "AnchorOne" , "AnchorTwo" , "AnchorThree" , "AnchorFour"
    ,"AnchorFive", "AnchorSix"};
    Vector3[] SpawnLocation;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NeedsNewSlice == 1)
        {
            NeedsNewSlice = 0;
            StartCoroutine(Spawn());
            IsRed = Random.Range(0,2);
        }
    }
    public void spawnSlice() 
    {
        int SpawnIndex = Random.Range(0, 6);
        Debug.Log("The index is" + SpawnIndex);
        //spawn a new slice at spawner
        if(IsRed == 1)
        {
            GameObject NewSlice = Instantiate(RedPrefab) as GameObject;
            Vector3 DropPoint = (GameObject.FindWithTag(Anchors[SpawnIndex])).transform.position;
            DropPoint.y += 5;
            NewSlice.transform.position = DropPoint;
            NewSlice.transform.rotation = (GameObject.FindWithTag(Anchors[SpawnIndex])).transform.rotation;
            NewSlice.tag = RedTags[SpawnIndex];
        }
        else 
        {
            GameObject NewSlice = Instantiate(YellowPrefab) as GameObject;
            Vector3 DropPoint = (GameObject.FindWithTag(Anchors[SpawnIndex])).transform.position;
            DropPoint.y += 5;
            NewSlice.transform.position = DropPoint;
            NewSlice.transform.rotation = (GameObject.FindWithTag(Anchors[SpawnIndex])).transform.rotation;
        }
        
        
    
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        NeedsNewSlice = 1;
        spawnSlice();
    }


}

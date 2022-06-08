using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject YellowPrefab;
    public GameObject RedPrefab;
    public int NeedsNewSlice = 1;
    public int NewSliceSpawnSeconds;
    public int IsRed = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NeedsNewSlice == 1)
        {
            StartCoroutine(NewSliceCheck());
            IsRed = Random.Range(0,2);
        }
    }
    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        if(IsRed == 1)
        {
            GameObject NewSlice = Instantiate(RedPrefab) as GameObject;
            NewSlice.transform.position = transform.position;
        }
        else 
        {
            GameObject NewSlice = Instantiate(YellowPrefab) as GameObject;
            NewSlice.transform.position = transform.position;
        }
        
        
    
    }
    IEnumerator NewSliceCheck()
    {
        NeedsNewSlice = 0;
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}
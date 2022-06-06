using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject pizzaslicePrefab;
    public int NeedsNewSlice = 1;
    public int NewSliceSpawnSeconds;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NeedsNewSlice == 1)
        {
            StartCoroutine(NewSliceCheck());
        }
    }
    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        GameObject NewSlice = Instantiate(pizzaslicePrefab) as GameObject;
        NewSlice.transform.position = transform.position;
    
    }
    IEnumerator NewSliceCheck()
    {
        NeedsNewSlice = 0;
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject Slice;
    public GameObject Bomb;
    public GameObject ColorChanger;

    public int NeedsNewSlice = 1;
    public int NewSliceSpawnSeconds;
    int SpawnRed;


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NeedsNewSlice == 1)
        {
            NeedsNewSlice = 0;
            StartCoroutine(NewSliceCheck());
            SpawnRed = Random.Range(0,2);
        }
    }

    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        GameObject NewSlice = Instantiate(Slice) as GameObject;
        NewSlice.transform.position = transform.position;
        if(SpawnRed == 1)
        {
            Debug.Log("WTF");
            NewSlice.GetComponent<PizzaRotation>().IsRed = 1;            
        }
    }
    
    IEnumerator NewSliceCheck()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}
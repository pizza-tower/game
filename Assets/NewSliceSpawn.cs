using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject YellowPrefab;
    public GameObject RedPrefab;
    public int NeedsNewSlice = 1;
    public int NewSliceSpawnSeconds;
    public int IsRed = 0;
    public int Level = 0;

    private int[] sliceSeq = {0,0,0};
    private int indexOfSlice = 0;
    
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

            if(Level == 0 && GlobalData.isFirstFusionOver==false){
                
                IsRed = sliceSeq[indexOfSlice];
                indexOfSlice += 1;
            }else {
                IsRed = Random.Range(0,2);
            }

            if(GlobalData.isFirstFusionOver == true)
            {
                GameObject ui_handler = GameObject.Find("UIHandler");
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Throw at the right moment! Remember that same colored slices fuses and scores!"));
            }

        }
    }

    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        if(IsRed == 1)
        {
            GameObject NewSlice = Instantiate(RedPrefab) as GameObject;
            NewSlice.transform.position = transform.position;
            //GetComponent<PizzaRotation>().StopRotate = 0;
        }
        else 
        {
            GameObject NewSlice = Instantiate(YellowPrefab) as GameObject;
            NewSlice.transform.position = transform.position;
            //GetComponent<PizzaRotation>().StopRotate = 0;
        }
    }
    
    IEnumerator NewSliceCheck()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}
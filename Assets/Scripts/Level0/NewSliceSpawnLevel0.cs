using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class NewSliceSpawnLevel0 : MonoBehaviour
{
    public GameObject Slice;

    public int NeedsNewSlice = 1;
    public float NewSliceSpawnSeconds;
    public int SpawnRed;

    private int[] sliceSeq = {0,0,0,1};
    private int indexOfSlice = 0;
    public int Level;
    private GameObject SpawnedSlice;

    public GameObject GetSpawnedSlice(){
        return SpawnedSlice;
    }

    void Start()
    {
        //SceneManager. LoadScene("level0");
        Level=SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("NeedsNewSlice: " + NeedsNewSlice);
        if(NeedsNewSlice == 1)
        {
            NeedsNewSlice = 0;
            StartCoroutine(NewSliceCheck());

            // if(GlobalData.isFirstFusionOver == true)
            // {
            //         Debug.Log("After first fusion : ");
            //         GameObject ui_handler = GameObject.Find("UIHandler");
            //         ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, 
            //         (x, y) => x.SetTutorialInstruction("Throw at the right moment! Remember that same colored slices fuses and scores!"));
            //         Level++;
            //         SceneManager.LoadScene(Level);
            // }
        }
    }

    public GameObject spawnSlice() 
    {
        //spawn a new slice at spawner
        GameObject NewSlice = Instantiate(Slice) as GameObject;
        NewSlice.transform.position = transform.position;

        if(SpawnRed == -1)
            SpawnRed =  Random.Range(0,2);
        
        // sliceSeq[indexOfSlice];
        // indexOfSlice += 1;
        // Analytics tracking for Slices Thrown
        
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "SlicesThrown",
            new Dictionary<string, object> {
                { "Level", SceneManager.GetActiveScene().name },
                { "Slice", SpawnRed }
            }
        );
        Debug.Log("analyticsResult (SlicesThrown): " + analyticsResult);
        Analytics.FlushEvents();

        if(SpawnRed == 1)
        {
            NewSlice.GetComponent<PizzaRotationLevel0>().IsRed = 1;
        }
        else if(SpawnRed == 0)
        {
            NewSlice.GetComponent<PizzaRotationLevel0>().IsRed = 0;   
        }

        SpawnedSlice = NewSlice;
        return NewSlice;
    }
    
    IEnumerator NewSliceCheck()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}
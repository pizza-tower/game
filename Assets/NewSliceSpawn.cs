using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject Slice;

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
            
        }
    }

    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        GameObject NewSlice = Instantiate(Slice) as GameObject;
        NewSlice.transform.position = transform.position;
        SpawnRed = Random.Range(0,2);
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
            NewSlice.GetComponent<PizzaRotation>().IsRed = 1;
        }
        else if(SpawnRed == 0)
        {
            NewSlice.GetComponent<PizzaRotation>().IsRed = 0;   
        }
    }
    
    IEnumerator NewSliceCheck()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}
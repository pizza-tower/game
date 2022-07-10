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
    public float NewSliceSpawnSeconds;
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

        List<SliceColor> sColors = GlobalData.ValidSlices[SceneManager.GetActiveScene().name];
        int n = sColors.Count;
        int r = Random.Range(0, n);
        SliceColor c = sColors[r];

        // Analytics tracking for Slices Thrown
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "SlicesThrown",
            new Dictionary<string, object> {
                { "Level", SceneManager.GetActiveScene().name },
                { "Slice", (int)c }
            }
        );
        Analytics.FlushEvents();

        NewSlice.GetComponent<PizzaRotation>().mColor = c;
    }
    
    IEnumerator NewSliceCheck()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }

}
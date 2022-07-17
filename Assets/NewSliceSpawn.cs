using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject Slice;
    float NewSliceSpawnSeconds;
    public int NeedsNewSlice = 1;
    public int NumberSpawned = 0;

    void Start()
    {
        NewSliceSpawnSeconds = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(NeedsNewSlice == 1)
        {
            if(NumberSpawned >= GlobalData.MaxSlices[SceneManager.GetActiveScene().name])
            {
                NeedsNewSlice = 0;
                // TODO: Trigger game over
                return;
            }
            NeedsNewSlice = 0;
            StartCoroutine(Spawn());
        }
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        NewSliceSpawnSeconds = 1.3f;
        spawnSlice();
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
        NumberSpawned++;
    }
    

}
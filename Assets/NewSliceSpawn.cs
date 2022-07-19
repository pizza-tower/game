using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using TMPro;

public class NewSliceSpawn : MonoBehaviour
{
    public GameObject Slice;
    public GameObject endlevelscreen;
    public GameObject winscreen;
    float NewSliceSpawnSeconds;
    public int NeedsNewSlice = 1;
    public int NumberSpawned = 0;
    bool instantiated = false;


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

                int level = SceneManager.GetActiveScene().buildIndex;
                if (instantiated == false && level + 1 < GlobalData.totalscenes ) {
                    GameObject popup = Instantiate(endlevelscreen);
                    instantiated = true;
                    GameObject star1 = popup.transform.GetChild(1).gameObject;
                    GameObject star2 = popup.transform.GetChild(2).gameObject;
                    GameObject star3 = popup.transform.GetChild(3).gameObject;
                    star1.SetActive(false);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    GameObject plate = GameObject.FindWithTag("Plate");
                    Score score =  plate.GetComponent<Score>();
                    print("STARS");
                    print(score.GetScoreSummary().starsEarned);
                    if (score.GetScoreSummary().starsEarned >= 1) {
                        star1.SetActive(true);
                    }
                    if (score.GetScoreSummary().starsEarned >= 2) {
                        star2.SetActive(true);
                    }
                    if (score.GetScoreSummary().starsEarned >= 3) {
                        star3.SetActive(true);
                    }
                }
                
                else if (instantiated == false) {
                    GameObject popup = Instantiate(endlevelscreen);
                    instantiated = true;
                    GameObject star1 = popup.transform.GetChild(1).gameObject;
                    GameObject star2 = popup.transform.GetChild(2).gameObject;
                    GameObject star3 = popup.transform.GetChild(3).gameObject;
                    star1.SetActive(false);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    GameObject plate = GameObject.FindWithTag("Plate");
                    Score score =  plate.GetComponent<Score>();
                    if (score.GetScoreSummary().starsEarned >= 1) {
                        star1.SetActive(true);
                    }
                    if (score.GetScoreSummary().starsEarned >= 2) {
                        star2.SetActive(true);
                    }
                    if (score.GetScoreSummary().starsEarned >= 3) {
                        star3.SetActive(true);
                    }
                }
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
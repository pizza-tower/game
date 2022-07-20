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
    public GameObject gameoverscreen;
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
                    StartCoroutine(PopUp(gameoverscreen));
         
                }
                
                else if (instantiated == false) {
                    StartCoroutine(PopUp(winscreen));
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
    IEnumerator PopUp(GameObject panel) {
        yield return new WaitForSeconds(2);
                    GameObject popup = Instantiate(panel);
                    instantiated = true;
                    GameObject star1 = popup.transform.GetChild(1).gameObject;
                    GameObject star2 = popup.transform.GetChild(2).gameObject;
                    GameObject star3 = popup.transform.GetChild(3).gameObject;
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
                    Transform vertscore = popup.transform.GetChild(4);
                    TextMeshProUGUI verttext = vertscore.gameObject.GetComponent<TextMeshProUGUI>();
                    verttext.SetText("# Vertical Fusions = {} x 5 = {} points", score.GetScoreSummary().numVerticalFusions, score.GetScoreSummary().scoreVerticalFusions);    

                    Transform horscore = popup.transform.GetChild(5);
                    TextMeshProUGUI hortext = horscore.gameObject.GetComponent<TextMeshProUGUI>();
                    hortext.SetText("# Horizontal Fusions = {} x 20 = {} points", score.GetScoreSummary().numHorizontalFusions, score.GetScoreSummary().scoreHorizontalFusions);    

                    Transform powscore = popup.transform.GetChild(6);
                    TextMeshProUGUI powtext = powscore.gameObject.GetComponent<TextMeshProUGUI>();
                    powtext.SetText("# Powers Used = {} x 5 = {} points", score.GetScoreSummary().numPowersUsed, score.GetScoreSummary().scorePowersUsed);    

                    Transform slicescore = popup.transform.GetChild(7);
                    TextMeshProUGUI slicetext = slicescore.gameObject.GetComponent<TextMeshProUGUI>();
                    slicetext.SetText("# Slices Left = {} x 5 = {} points", score.GetScoreSummary().numSlicesLeft, score.GetScoreSummary().scoreSlicesLeft);    

                    Transform totalscore = popup.transform.GetChild(8);
                    TextMeshProUGUI totaltext = totalscore.gameObject.GetComponent<TextMeshProUGUI>();
                    totaltext.SetText("Total Score = {} points", score.GetScoreSummary().scoreTotal);    

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
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
    public GameObject gameoverscreen;
    float NewSliceSpawnSeconds;
    public int NeedsNewSlice = 1;
    public int NumberSpawned = 0;
    private GameObject SpawnedSlice;

    public GameObject GetSpawnedSlice(){
        return SpawnedSlice;
    }
    bool instantiated = false;

    public Mesh RedSlice;
    public Mesh YellowSlice;
    public Mesh BlueSlice;
    public Mesh GreenSlice;
    public Mesh BrownSlice;


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

                if (instantiated == false ) {
                    StartCoroutine(PopUp(gameoverscreen));

         
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
                int level = SceneManager.GetActiveScene().buildIndex;
                if (level + 1 >= GlobalData.totalscenes) {
                    popup.transform.GetChild(13).gameObject.SetActive(false);
                }
                GameObject star1 = popup.transform.GetChild(1).gameObject;
                GameObject star2 = popup.transform.GetChild(2).gameObject;
                GameObject star3 = popup.transform.GetChild(3).gameObject;
                GameObject plate = GameObject.FindWithTag("Plate");
                ScoreSummary s =  Score.GetScoreSummary();
                if (s.starsEarned >= 1) {
                    star1.SetActive(true);
                }
                if (s.starsEarned >= 2) {
                    star2.SetActive(true);
                }
                if (s.starsEarned >= 3) {
                    star3.SetActive(true);
                }
                Transform vertscore = popup.transform.GetChild(4);
                TextMeshProUGUI verttext = vertscore.gameObject.GetComponent<TextMeshProUGUI>();
                verttext.SetText(string.Format("# Vertical Fusions = {0} x 5 = {1} points", s.numVerticalFusions, s.scoreVerticalFusions));    

                Transform horscore = popup.transform.GetChild(5);
                TextMeshProUGUI hortext = horscore.gameObject.GetComponent<TextMeshProUGUI>();
                hortext.SetText(string.Format("# Horizontal Fusions = {0} x 20 = {1} points", s.numHorizontalFusions, s.scoreHorizontalFusions));    

                Transform powscore = popup.transform.GetChild(6);
                TextMeshProUGUI powtext = powscore.gameObject.GetComponent<TextMeshProUGUI>();
                powtext.SetText(string.Format("# Powers Used = {0} x 5 = {1} points", s.numPowersUsed, s.scorePowersUsed));    

                Transform slicescore = popup.transform.GetChild(7);
                TextMeshProUGUI slicetext = slicescore.gameObject.GetComponent<TextMeshProUGUI>();
                slicetext.SetText(string.Format("# Slices Left = {0} x 1 = {1} points", s.numSlicesLeft, s.scoreSlicesLeft));    

                Transform totalscore = popup.transform.GetChild(8);
                TextMeshProUGUI totaltext = totalscore.gameObject.GetComponent<TextMeshProUGUI>();
                totaltext.SetText(string.Format("Total Score = {0} points", s.scoreTotal));  

    }
    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        GameObject NewSlice = Instantiate(Slice) as GameObject;
        NewSlice.tag = "NS";
        NewSlice.transform.position = transform.position;
        NewSlice.transform.localScale = new Vector3(2, 2, 2);

        List<SliceColor> sColors = GlobalData.ValidSlices[SceneManager.GetActiveScene().name];
        int n = sColors.Count;
        int r = Random.Range(0, n);
        SliceColor c = sColors[r];

        NewSlice.GetComponent<MeshFilter>().sharedMesh = SliceColorToMesh(c);
        NewSlice.GetComponent<PizzaRotation>().mColor = c;
        NumberSpawned++;

        SpawnedSlice = NewSlice;
        
    }
    
    public Mesh SliceColorToMesh(SliceColor c)
    {
        switch(c)
        {
            case SliceColor.Red:
                return Instantiate(RedSlice);
            case SliceColor.Yellow:
                return Instantiate(YellowSlice);
            case SliceColor.Blue:
                return Instantiate(BlueSlice);
            case SliceColor.Green:
                return Instantiate(GreenSlice);
            case SliceColor.DarkBrown:
                return Instantiate(BrownSlice);
            default:
                return Instantiate(RedSlice);
        }
    }
}
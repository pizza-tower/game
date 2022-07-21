using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using TMPro;


public class FuseSlice : MonoBehaviour
{
    public GameObject completeScreen;

    private static bool mCheckTopNSlices(List<GameObject> SList)
    {
        int n = GlobalData.verticalFusionHeight;

        //Debug.Log("Inside check slices");
        //if the height of tower is shorter than n, return false
        if (n > SList.Count || SList.Count == 0)
        {
            return false;
        }

        //if any slice of the top 'n' slices is not of desired color, return false
        //else return true
        SliceColor c1 = SList[SList.Count - 1].GetComponent<PizzaRotation>().mColor;
        SliceColor c2 = SList[SList.Count - 2].GetComponent<PizzaRotation>().mColor;
        SliceColor c3 = SList[SList.Count - 3].GetComponent<PizzaRotation>().mColor;
        
        // If all slices are of the same color then return true
        if (c1 == c2 && c1 == c3) return true;

        // Not of same color
        return false;
    }

    public static int mVertFuse(List<GameObject> SList)
    {
        int n = GlobalData.verticalFusionHeight;

        //Check if top n slices in have same color as the latest slice
        if (mCheckTopNSlices(SList))
        {
            // GameObject.Find("smoke").GetComponentInChildren<ParticleSystem>().Play();
            // Reference: https://www.youtube.com/watch?v=jQivfs34Wb0
            
            // Instantiate(smoking,0,0);

            // animator.SetTrigger("isSmoking");

            //Debug.Log("Slices were same colored");


            for (int k = 1; k <= n; k++)
            {
                Destroy(SList[SList.Count - k]);
            }
            GlobalData.nVerticalFusions++;
            Score.numVerticalFuses++;
            SList.RemoveRange(SList.Count - n, n);
            return 1;
        }
        return 0;
    }

    /*Bomb will fuse all slice in that SList
    */
    public static void BombFuse(List<GameObject> SList)
    {
        int n = SList.Count;
        if(n>0){
            
            AudioSource audioData;
            audioData = GameObject.Find("AnchorOne").GetComponent<AudioSource>();
            audioData.Play(0);
        }
        if(n > 3)
        {
            Destroy(SList[n-1]);
            Destroy(SList[n-2]);
            Destroy(SList[n-3]);
            SList.RemoveRange(n - 3, 3);
        }
        else
        {
            for (int k = 0; k < SList.Count; k++)
            {
                Destroy(SList[k]);
            }
            SList.RemoveRange(0, n);
        }
        //Destroy(GameObject.FindWithTag("0"));
        Debug.Log("boom");
        // GameObject.Find("flame").GetComponentInChildren<ParticleSystem>().Play();
        // ReferenceEquals:https://www.youtube.com/watch?v=zJFcCngLP-Q
    }

    /* Logic for horizontal fusion:
    Author: Manasi
    Use a global list which contains all 6 (vertical) lists.
    Find the minimum height from all the lists since the pizza could be only formed at that level and not above.
    Assumption: If checking for pizza at ith level then it means that all the i-1 levels have been already checked.
    
    Once the minimum height is found, iterate over all lists and check the color of slices at that height
    if same, then destroy the slices and shift all the slices down by one level which are above the min height.

    */
    public static int mHorizontalFuse()
    {
        List<List<GameObject>> glist = GlobalData.globalList;
        int m = glist[0].Count;
        for (int i = 1; i < 6; i++) if (glist[i].Count < m) m = glist[i].Count;
        if (m == 0) return 0;

        SliceColor c1 = glist[0][m-1].GetComponent<PizzaRotation>().mColor;
        SliceColor c2 = glist[1][m-1].GetComponent<PizzaRotation>().mColor;
        SliceColor c3 = glist[2][m-1].GetComponent<PizzaRotation>().mColor;
        SliceColor c4 = glist[3][m-1].GetComponent<PizzaRotation>().mColor;
        SliceColor c5 = glist[4][m-1].GetComponent<PizzaRotation>().mColor;
        SliceColor c6 = glist[5][m-1].GetComponent<PizzaRotation>().mColor;

        //Compute all rotations
        List<List<SliceColor>> rotations = new()
        { 
            new() { c1, c2, c3, c4, c5, c6 },
            new() { c2, c3, c4, c5, c6, c1 },
            new() { c3, c4, c5, c6, c1, c2 },
            new() { c4, c5, c6, c1, c2, c3 },
            new() { c5, c6, c1, c2, c3, c4 },
            new() { c6, c1, c2, c3, c4, c5 }
        };

        // Check if any rotation is a valid combination
        for(int i = 0; i < 6; i++)
        {
            int r = CheckPizza(rotations[i]);
            if(r != -1)
            {

                //audio
                AudioSource audioData;
                audioData = GameObject.Find("Plate").GetComponent<AudioSource>();
                audioData.Play(0);

                //Fuse possible! No need to loop any further
                for(int j = 0; j < 6; j++)
                {
                    //Shift all slices down
                    for (int k = m; k < glist[j].Count; k++)
                    {
                        glist[j][k].transform.Translate(0f, -0.2f, 0f);
                    }
                    //Destroy the pizza slice part of fusing
                    Destroy(glist[j][m - 1]);
                    glist[j].RemoveAt(m - 1);
                }
                GlobalData.isHorizontalFuse = true;
                GlobalData.nHorizontalFusions++;
                Score.numHorizontalFuses++;

                HandleReward(r);
                return r;
            }
        }
        
        return -1;
    }

    /*
     * Checks if a passed list of six slices is a valid pizza in the Global list of valid combinations for the current scene
     * Returns the index into the list of valid combinations if there is a match
     * Returns -1 if there is no match
     */
    public static int CheckPizza(List<SliceColor> pizza)
    {
        List<List<SliceColor>> combinations = GlobalData.ValidCombinations[SceneManager.GetActiveScene().name];

        for(int i = 0; i < combinations.Count; i++)
        {
            bool matches = true;
            for(int j = 0; j < 6; j++)
            {
                matches = matches && (combinations[i][j] == pizza[j]);
            }
            if (matches) return i;
        }
        return -1;
    }

          
    /*TODO: Write this function to give score, update level requirements etc*/
    public static void HandleReward(int fuseIndex) {
        Debug.Log("Horizontal fusion acknowledged: Level: " + SceneManager.GetActiveScene().name + " Combination index: " + fuseIndex.ToString());
        Score.fusionsMade[fuseIndex]++;
        for(int i = 0; i < Score.fusionsMade.Count; i++)
        {
            if (Score.fusionsMade[i] == 0) return;
        }
        //TODO: You win!! CODE HERE
        Debug.Log("YOU WIN");
        ShowLevelComplete();
    }


    public static void ShowLevelComplete()
    {
        GameObject popup = Instantiate(GameObject.Find("PizzaSpawner").GetComponent<FuseSlice>().completeScreen);
        int level = SceneManager.GetActiveScene().buildIndex;
        if (level + 1 >= GlobalData.totalscenes)
        {
            popup.transform.GetChild(13).gameObject.SetActive(false);
        }
        GameObject star1 = popup.transform.GetChild(1).gameObject;
        GameObject star2 = popup.transform.GetChild(2).gameObject;
        GameObject star3 = popup.transform.GetChild(3).gameObject;
        GameObject plate = GameObject.FindWithTag("Plate");
        ScoreSummary s = Score.GetScoreSummary();
        Debug.Log("S: " + s.scorePowersUsed);
        if (s.starsEarned >= 1)
        {
            star1.SetActive(true);
        }
        if (s.starsEarned >= 2)
        {
            star2.SetActive(true);
        }
        if (s.starsEarned >= 3)
        {
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


    // IEnumerator PopUp(GameObject panel) {
    //     yield return new WaitForSeconds(2);
    //                 GameObject popup = Instantiate(panel);
    //                 bool instantiated = true;
    //                 int level = SceneManager.GetActiveScene().buildIndex;
    //                 if (level + 1 >= GlobalData.totalscenes) {
    //                     popup.transform.GetChild(13).gameObject.SetActive(false);  
    //                 }
    //                 GameObject star1 = popup.transform.GetChild(1).gameObject;
    //                 GameObject star2 = popup.transform.GetChild(2).gameObject;
    //                 GameObject star3 = popup.transform.GetChild(3).gameObject;
    //                 GameObject plate = GameObject.FindWithTag("Plate");
    //                 Score score =  plate.GetComponent<Score>();
    //                 if (score.GetScoreSummary().starsEarned >= 1) {
    //                     star1.SetActive(true);
    //                 }
    //                 if (score.GetScoreSummary().starsEarned >= 2) {
    //                     star2.SetActive(true);
    //                 }
    //                 if (score.GetScoreSummary().starsEarned >= 3) {
    //                     star3.SetActive(true);
    //                 }
    //                 Transform vertscore = popup.transform.GetChild(4);
    //                 TextMeshProUGUI verttext = vertscore.gameObject.GetComponent<TextMeshProUGUI>();
    //                 verttext.SetText("# Vertical Fusions = {} x 5 = {} points", score.GetScoreSummary().numVerticalFusions, score.GetScoreSummary().scoreVerticalFusions);    

    //                 Transform horscore = popup.transform.GetChild(5);
    //                 TextMeshProUGUI hortext = horscore.gameObject.GetComponent<TextMeshProUGUI>();
    //                 hortext.SetText("# Horizontal Fusions = {} x 20 = {} points", score.GetScoreSummary().numHorizontalFusions, score.GetScoreSummary().scoreHorizontalFusions);    

    //                 Transform powscore = popup.transform.GetChild(6);
    //                 TextMeshProUGUI powtext = powscore.gameObject.GetComponent<TextMeshProUGUI>();
    //                 powtext.SetText("# Powers Used = {} x 5 = {} points", score.GetScoreSummary().numPowersUsed, score.GetScoreSummary().scorePowersUsed);    

    //                 Transform slicescore = popup.transform.GetChild(7);
    //                 TextMeshProUGUI slicetext = slicescore.gameObject.GetComponent<TextMeshProUGUI>();
    //                 slicetext.SetText("# Slices Left = {} x 5 = {} points", score.GetScoreSummary().numSlicesLeft, score.GetScoreSummary().scoreSlicesLeft);    

    //                 Transform totalscore = popup.transform.GetChild(8);
    //                 TextMeshProUGUI totaltext = totalscore.gameObject.GetComponent<TextMeshProUGUI>();
    //                 totaltext.SetText("Total Score = {} points", score.GetScoreSummary().scoreTotal);    
    //                 yield return popup;

    // }
}

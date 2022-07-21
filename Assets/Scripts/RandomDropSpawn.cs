using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class RandomDropSpawn : MonoBehaviour
{
    
    public GameObject Slice;
    public int NeedsNewSlice = 1;
    public int NewSliceSpawnSeconds;
    string[] Anchors = new string[] { "AnchorOne" , "AnchorTwo" , "AnchorThree" , "AnchorFour"
    ,"AnchorFive", "AnchorSix"};

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NeedsNewSlice == 1)
        {
            NeedsNewSlice = 0;
            StartCoroutine(Spawn());
        }
    }
    public void spawnSlice() 
    {
        //spawn a new slice at spawner
        GameObject NewSlice = Instantiate(Slice) as GameObject;
        NewSlice.transform.position = transform.position;
        NewSlice.GetComponent<PizzaRotation>().IsRandomDrop = true;
        List<SliceColor> sColors = GlobalData.ValidSlices[SceneManager.GetActiveScene().name];
        int n = sColors.Count;
        int r = Random.Range(0, n);
        SliceColor c = sColors[r];
        NewSlice.GetComponent<PizzaRotation>().mColor = c;
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(NewSliceSpawnSeconds);
        spawnSlice();
    }


}

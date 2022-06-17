using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceList : MonoBehaviour
{
    public List<GameObject> SList = new List<GameObject>();
    public static List<List<GameObject>> globalList = new List<List<GameObject>>();
    
    //public float SliceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        globalList.Add(SList);
    }
    /*public void IncrementCount()
    {
        SliceCount += 1;
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCodedSlice : MonoBehaviour
{

    public GameObject YellowPrefab;
    private GameObject AnchorToFind;
    private string ListToAdd = "AnchorOne";
    private SliceList List;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "R_1" ||gameObject.tag == "Y_1")
        {
            AnchorToFind = GameObject.FindWithTag("AnchorOne");
            List = AnchorToFind.GetComponent<SliceList>();
            List.SList.Add(gameObject);

        }
        if(gameObject.tag == "R_2" ||gameObject.tag == "Y_2")
        {
            AnchorToFind = GameObject.FindWithTag("AnchorTwo");
            List = AnchorToFind.GetComponent<SliceList>();
            List.SList.Add(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

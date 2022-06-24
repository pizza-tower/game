using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
    //there are four materials, yellow, red, yellow transparent, red transparent
    public Material[] MaterialList;
    Renderer Rend;
    // Start is called before the first frame update
    void Start()
    {
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
        //be default the material is yellow
        Rend.sharedMaterial = MaterialList[0];

    }
    public void ToRed()
    {
        Rend.sharedMaterial = MaterialList[1];
    }
    public void ToYellow()
    {
        Rend.sharedMaterial = MaterialList[0];
    }
    public void ToYellowTransparent()
    {
        Rend.sharedMaterial = MaterialList[2];
    }
    public void ToRedTransparent()
    {
        Rend.sharedMaterial = MaterialList[3];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

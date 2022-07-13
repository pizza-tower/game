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
        //Rend.sharedMaterial = MaterialList[0];

    }
    public void ToYellow()
    {
        Rend.sharedMaterial = MaterialList[0];
    }
    public void ToRed()
    {
        Rend.sharedMaterial = MaterialList[1];
    }
    public void ToYellowTransparent()
    {
        Rend.sharedMaterial = MaterialList[2];
    }
    public void ToRedTransparent()
    {
        Rend.sharedMaterial = MaterialList[3];
    }
     public void ToBrownTransparent()
    {
        Rend.sharedMaterial = MaterialList[3];
    }
    public void ToBomb()
    {
        Rend.sharedMaterial = MaterialList[4];
    }
    public void ToRainbow()
    {
        Rend.sharedMaterial = MaterialList[5];
    }
    public void ToBrown()
    {
        Rend.sharedMaterial = MaterialList[6];
    }
    public void FlipColor()
    {
        SliceColor c = GetComponent<PizzaRotation>().mColor;
        if(c == SliceColor.Red || c == SliceColor.Brown)
        {
            //Brown, Red slices will be turned yellow when colorchanger slice is thrown.
            ToYellow();
            GetComponent<PizzaRotation>().mColor = SliceColor.Yellow;
        }
        // else if(GetComponent<PizzaRotation>().IsBrown != 1)
        else
        {
            //Yellow slice
            ToRed();
            GetComponent<PizzaRotation>().mColor = SliceColor.Red;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

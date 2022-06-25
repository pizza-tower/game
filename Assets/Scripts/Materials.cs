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
    public void ToBomb()
    {
        Rend.sharedMaterial = MaterialList[4];
    }
    public void ToRainbow()
    {
        Rend.sharedMaterial = MaterialList[5];
    }
    public void FlipColor()
    {
        if(GetComponent<PizzaRotation>().IsRed == 1)
        {
            ToYellow();
            GetComponent<PizzaRotation>().IsRed = 0;
            if(gameObject.tag == "R_1")
            {
                gameObject.tag = "Y_1";
            }
            else if (gameObject.tag == "R_2")
            {
                gameObject.tag = "Y_2";
            }
            else if (gameObject.tag == "R_3")
            {
                gameObject.tag = "Y_3";
            }
            else if (gameObject.tag == "R_4")
            {
                gameObject.tag = "Y_4";
            }
            else if (gameObject.tag == "R_5")
            {
                gameObject.tag = "Y_5";
            }
            else if (gameObject.tag == "R_6")
            {
                gameObject.tag = "Y_6";
            }
        }
        else
        {
            GetComponent<PizzaRotation>().IsRed = 1;
            ToRed();
            if(gameObject.tag == "Y_1")
            {
                gameObject.tag = "R_1";
            }
            else if (gameObject.tag == "Y_2")
            {
                gameObject.tag = "R_2";
            }
            else if (gameObject.tag == "Y_3")
            {
                gameObject.tag = "R_3";
            }
            else if (gameObject.tag == "Y_4")
            {
                gameObject.tag = "R_4";
            }
            else if (gameObject.tag == "Y_5")
            {
                gameObject.tag = "R_5";
            }
            else if (gameObject.tag == "Y_6")
            {
                gameObject.tag = "R_6";
            }
        }
        //Destroy(GameObject.FindWithTag("0"));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

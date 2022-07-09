using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotation : MonoBehaviour
{
    public int IsRotating = 1;
    public int StopRotate = 0;
    public SliceColor mColor = SliceColor.None;
    public int TagInInt;
    private bool AssignMaterial = false;
    public bool hardcoded = false;

    // Start is called before the first frame update
    void Start()
    {
        if(hardcoded!=true){
        //tag in integer, corresponding to the Tag R_1 R_2...
        TagInInt = Random.Range(0,6);
        //random spawn initial direction
        float InitialRotation = (float)TagInInt * (float)60.0;
        transform.Rotate(0, InitialRotation, 0);
       }      
      
    }


    // Update is called once per frame
    void Update()
    {
        if(AssignMaterial == false)
        {
             if(mColor == SliceColor.Brown)
            {
                gameObject.GetComponent<Materials>().ToBrown();
            }
            else if(mColor == SliceColor.Red)
            {
                gameObject.GetComponent<Materials>().ToRed();
            }
            else if (mColor == SliceColor.Yellow)
            {
                gameObject.GetComponent<Materials>().ToYellow();   
            }
            else
            {
                Debug.LogError("Invalid color code. Received: " + ((int)mColor).ToString());
            }
           
            AssignMaterial = true;
        }
        if(IsRotating == 1  && StopRotate == 0 && hardcoded!=true)
        {
            StartCoroutine(Rotate());
        }
        if(StopRotate == 1)
        {
            IsRotating = 0;
        }

    }
    IEnumerator Rotate()
    {
        IsRotating = 0;
        if(TagInInt ==  2|| TagInInt == 3)
        {
            if(GetComponent<PizzaParabola>().IsBomb == false && GetComponent<PizzaParabola>().IsColorChanger == false)
            {
                GlobalData.GoTransparent = 1;
            }
                
        }
        else 
        {
            GlobalData.GoTransparent = 0;
        }
        
        transform.Rotate(0, 60, 0);
        TagInInt += 1;
        TagInInt = TagInInt % 6;
 
        yield return new WaitForSeconds((float)0.6);
        IsRotating = 1;
    }

    public void MaterialToNormal()
    {
        switch(mColor)
        {
            case SliceColor.Red:
                GetComponent<Materials>().ToRed();
                break;
            case SliceColor.Yellow:
                GetComponent<Materials>().ToYellow();
                break;
            case SliceColor.Brown:
                GetComponent<Materials>().ToBrown();
                break;
        }
    }
    public void MaterialToTransparent()
    {
        switch (mColor)
        {
            case SliceColor.Red:
                GetComponent<Materials>().ToRedTransparent();
                break;
            case SliceColor.Yellow:
                GetComponent<Materials>().ToYellowTransparent();
                break;
            case SliceColor.Brown:
                GetComponent<Materials>().ToBrownTransparent();
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotation : MonoBehaviour
{
    public int IsRotating = 1;
    public int StopRotate = 0;
    public int IsRed;
    public int TagInInt;
    // Start is called before the first frame update
    void Start()
    {
        //tag in integer, corresponding to the Tag R_1 R_2...
        TagInInt = Random.Range(0,6);
        //random spawn initial direction
        float InitialRotation = (float)TagInInt * (float)60.0;
        transform.Rotate(0, InitialRotation, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRotating == 1  && StopRotate == 0)
        {
            StartCoroutine(Rotate());
        }
    }
    IEnumerator Rotate()
    {
        IsRotating = 0;
        if(IsRed == 1)
        {
            gameObject.GetComponent<Materials>().ToRed();

        } 
        else 
        {
            gameObject.GetComponent<Materials>().ToYellow();
        }
        
        if(TagInInt ==  2|| TagInInt == 3|| TagInInt ==  4)
        {
            GlobalData.GoTransparent = 1;
        }
        else 
        {
            GlobalData.GoTransparent = 0;
        }
        transform.Rotate(0, 60, 0);
        TagInInt += 1;
        TagInInt = TagInInt % 6;
        yield return new WaitForSeconds((float)0.8);
        IsRotating = 1;
    }
}
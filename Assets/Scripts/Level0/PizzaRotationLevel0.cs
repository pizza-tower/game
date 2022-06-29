using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotationLevel0 : MonoBehaviour
{
    public int IsRotating = 1;
    public int StopRotate = 0;
    public int IsRed;
    public int IsBrown=0;
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
            if(IsBrown == 1)
            {
                gameObject.GetComponent<MaterialsLevel0>().ToBrown();
            }
            else if(IsRed == 1)
            {
                gameObject.GetComponent<MaterialsLevel0>().ToRed();
            }
            else 
            {
                gameObject.GetComponent<MaterialsLevel0>().ToYellow();   
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
        if(TagInInt ==  2|| TagInInt == 3|| TagInInt ==  4)
        {
            if(GetComponent<PizzaParabolaLevel0>().IsBomb == false && GetComponent<PizzaParabolaLevel0>().IsColorChanger == false)
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
}
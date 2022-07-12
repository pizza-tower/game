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

    [SerializeField] [Range(0f, 6f)] float lerpRotationTime;
    [SerializeField] [Range(0f, 8f)] float lerpPositionTime;
    [SerializeField] Vector3[] myAngles;
    [SerializeField] Vector3[] myPositions;

    int angleIndex;
    int positionIndex; 
    int CanChangeTag = 1;
    int lenAng;
    int lenPos;
    float t1 = 0f;
    float t2 = 0f;
    int RotateCount = 0;
    public int ReadyToThrow = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(hardcoded!=true){
        //tag in integer, corresponding to the Tag R_1 R_2...
        TagInInt = Random.Range(0,6);
        //random spawn initial direction
        float InitialRotation = (float)TagInInt * (float)60.0;
        transform.Rotate(0, InitialRotation, 0);
        angleIndex = TagInInt * 3;
        positionIndex = TagInInt * 3;
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
            //flip and rotate animation
            
            transform.position = Vector3.Lerp(transform.position, myPositions[positionIndex], lerpPositionTime * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myAngles[angleIndex]), lerpRotationTime * Time.deltaTime);


            t1= Mathf.Lerp(t1, 1.0f, lerpPositionTime * Time.deltaTime);
            t2= Mathf.Lerp(t2, 1.0f, lerpRotationTime * Time.deltaTime);
            if(t1>0.9f)
            {
                t1 = 0f;
                positionIndex += 1;
                positionIndex %= 18;
            }
            if(t2 > 0.45f && RotateCount == 1 && CanChangeTag == 1)
            {
                CanChangeTag = 0;
                TagInInt += 1;
                TagInInt %= 6;
            }
            if(t2>0.9f)
            {
                t2 = 0f;
                angleIndex += 1;
                angleIndex %= 18;
                RotateCount += 1;
            }
            if(RotateCount >= 3)
            {
                RotateCount = 0;
                CanChangeTag = 1;
            }
            
            
        }
        
        //go transparent for Stack 2 and Stack 3 when the back row stacks are high enough
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
        if(StopRotate == 1)
        {
            IsRotating = 0;
        }
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
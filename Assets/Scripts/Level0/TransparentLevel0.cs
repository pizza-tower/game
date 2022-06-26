using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentLevel0 : MonoBehaviour
{
    int IsTransparent = 0;
    // Start is called before the first frame update
    List<GameObject> StackOne;
    List<GameObject> StackTwo;
    List<GameObject> StackThree;
    void Start()
    {
        Debug.Log(GlobalData.globalList.Count);
        StackOne = GlobalData.globalList[0];
        StackTwo = GlobalData.globalList[1];
        StackThree = GlobalData.globalList[2];
    }
    void BackToNormal()
    {
        IsTransparent = 0;
        for(int i = 0; i < StackOne.Count; i++)
        {
            if(StackOne[i].GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                StackOne[i].GetComponent<MaterialsLevel0>().ToRed();
            }
            else 
            {
                StackOne[i].GetComponent<MaterialsLevel0>().ToYellow();
            }
        }
        for(int i = 0; i < StackTwo.Count; i++)
        {
            if(StackTwo[i].GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                StackTwo[i].GetComponent<MaterialsLevel0>().ToRed();
            }
            else 
            {
                StackTwo[i].GetComponent<MaterialsLevel0>().ToYellow();
            }
        }
        for(int i = 0; i < StackThree.Count; i++)
        {
            if(StackThree[i].GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                StackThree[i].GetComponent<MaterialsLevel0>().ToRed();
            }
            else 
            {
                StackThree[i].GetComponent<MaterialsLevel0>().ToYellow();
            }
        }
    }
    void ToTransparent()
    {
        IsTransparent = 1;
        //only change the 1,2,3 stack, and only when the stack height >= 4;
        if(StackOne.Count >= 4)
        {
            for(int i = 0; i < StackOne.Count; i++)
            {
                if(StackOne[i].GetComponent<PizzaRotationLevel0>().IsRed == 1)
                {
                    StackOne[i].GetComponent<MaterialsLevel0>().ToRedTransparent();
                }
                else 
                {
                    StackOne[i].GetComponent<MaterialsLevel0>().ToYellowTransparent();
                }
            }        
        }

        if(StackTwo.Count >= 4)
        {
            for(int i = 0; i < StackTwo.Count; i++)
            {
                if(StackTwo[i].GetComponent<PizzaRotationLevel0>().IsRed == 1)
                {
                    StackTwo[i].GetComponent<MaterialsLevel0>().ToRedTransparent();
                }
                else 
                {
                    StackTwo[i].GetComponent<MaterialsLevel0>().ToYellowTransparent();
                }
            }        
        }
        if(StackThree.Count >= 4)
        {
            for(int i = 0; i < StackThree.Count; i++)
            {
                if(StackThree[i].GetComponent<PizzaRotationLevel0>().IsRed == 1)
                {
                    StackThree[i].GetComponent<MaterialsLevel0>().ToRedTransparent();
                }
                else 
                {
                    StackThree[i].GetComponent<MaterialsLevel0>().ToYellowTransparent();
                }
            }        
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(GlobalData.GoTransparent == 1 && IsTransparent == 0)
        {
            ToTransparent();
        }
        if(GlobalData.GoTransparent == 0 && IsTransparent == 1)
        {
            BackToNormal();
        }
    }
}

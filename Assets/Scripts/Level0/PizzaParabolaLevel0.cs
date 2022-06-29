
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine. SceneManagement;
using UnityEngine.Analytics;

public class PizzaParabolaLevel0 : MonoBehaviour
{
    private int Level;
    // Start is called before the first frame update
    protected float Animation = 0;
    Vector3 StartPoint;
    Vector3 EndPoint;
    private float IsThrowing = 0;
    private int TargetList = 0;
    private string TargetAnchor = "";
    private GameObject AnchorToFind;
    private SliceList List;
    bool IsPlaced = false;  
    public bool IsBomb = false;
    public bool IsColorChanger = false;

    public bool GetIsPlaced(){
        return IsPlaced;
    }

    void Start()
    {
        StartPoint = (GameObject.FindWithTag("Spawner")).transform.position;

    }
    // Update is called once per frame
    void AddToList()
    {
        string tag = gameObject.tag;   
         if (tag == "R_1" || tag == "Y_1")
        {
            TargetList = 0;
            TargetAnchor = "AnchorOne";
        }
        else if (tag == "R_2" || tag == "Y_2")
        {
            TargetList = 1;
            TargetAnchor = "AnchorTwo";
        }
        else if (tag == "R_3" || tag == "Y_3")
        {
            TargetList = 2;
            TargetAnchor = "AnchorThree";
        }
        else if (tag == "R_4" || tag == "Y_4")
        {
            TargetList = 3;
            TargetAnchor = "AnchorFour";
        }
        else if (tag == "R_5" || tag == "Y_5")
        {
            TargetList = 4;
            TargetAnchor = "AnchorFive";
        }
        else if (tag == "R_6" || tag == "Y_6")
        {
            TargetList = 5;
            TargetAnchor = "AnchorSix";
        }
        //only add the gameobject to the list when it is a slice
        if(IsBomb == false && IsColorChanger == false)
        {
            GlobalData.globalList[TargetList].Add(gameObject);
        }
        


    }
    public void AssignTag()
    {
        string tag = gameObject.tag;  
        // if (GlobalData.isFirstSlice == false && GlobalData.isFirstFusionOver==false)
        // {
        //     if(isRotationToBeStopped(tag))
        //     {
        //         StopRotation();
        //         Debug.Log("Rotation stopped");
        //         // GameObject ui_handler = GameObject.Find("UIHandler");
        //         // ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Press SPACE BAR NOW!!  Will the slices fuse? Lets see!"));
        //     }
        // }
        // if (GlobalData.isFirstSlice == false && GlobalData.isFirstFusionOver == true && GlobalData.isFirstHorizontalFusionOver == false)
        // {
        //     if (isRotationToBeStoppedForHorizontalFusion(tag))
        //     {
        //         StopRotation();
        //         Debug.Log("Rotation stopped");
        //         // GameObject ui_handler = GameObject.Find("UIHandler");
        //         // ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Press SPACE BAR NOW!! Lets see if we can make the similar colored slices at the same level disappear!"));
        //     }
        // }

        if(GetComponent<PizzaRotationLevel0>().TagInInt == 0)
        {
            if(GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                gameObject.tag = "R_1";
            }
            else 
            {
                gameObject.tag = "Y_1";
            }
        }
        else if (GetComponent<PizzaRotationLevel0>().TagInInt == 1)
        {
            if(GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                gameObject.tag = "R_2";
            }
            else 
            {
                gameObject.tag = "Y_2";
            }
        }
        else if (GetComponent<PizzaRotationLevel0>().TagInInt == 2)
        {
            if(GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                gameObject.tag = "R_3";
            }
            else 
            {
                gameObject.tag = "Y_3";
            }
        }
        else if (GetComponent<PizzaRotationLevel0>().TagInInt == 3)
        {
            if(GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                gameObject.tag = "R_4";
            }
            else 
            {
                gameObject.tag = "Y_4";
            }
        }
        else if (GetComponent<PizzaRotationLevel0>().TagInInt == 4)
        {
            if(GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                gameObject.tag = "R_5";
            }
            else 
            {
                gameObject.tag = "Y_5";
            }
        }
        else if (GetComponent<PizzaRotationLevel0>().TagInInt == 5)
        {
            if(GetComponent<PizzaRotationLevel0>().IsRed == 1)
            {
                gameObject.tag = "R_6";
            }
            else 
            {
                gameObject.tag = "Y_6";
            }
        }
    }
    void Bomb()
    {
        IsBomb = false;
        FuseSliceLevel0.BombFuse(GlobalData.globalList[TargetList]);
        
        Destroy(gameObject);
    }
    void ChangeColor()
    {
        IsColorChanger = false;
        if(GlobalData.globalList[TargetList].Count >= 1)
        {
            GlobalData.globalList[TargetList][GlobalData.globalList[TargetList].Count - 1].GetComponent<MaterialsLevel0>().FlipColor();
        }
        FuseSliceLevel0.mHorizontalFuse();
        FuseSliceLevel0.mVertFuse(GlobalData.globalList[TargetList]);
        
        Destroy(gameObject);
    }
    void StopRotation()
    {
        GetComponent<PizzaRotationLevel0>().StopRotate = 1;
    }

    void ResumeRotation()
    {
        GetComponent<PizzaRotationLevel0>().StopRotate = 0;
    }

    void ThrowSlice()
    {
        
        EndPoint = (GameObject.FindWithTag(TargetAnchor)).transform.position;
        float Count = GlobalData.globalList[TargetList].Count;
        EndPoint.y += 0.2f * Count;
        //Debug.Log($"Position: {EndPoint}"); 
    }
    void Update()
    {
        if(IsPlaced)
        {
            GlobalData.isFirstSlice = false;
            // Debug.Log("First SLice done");
            return;
        }
        
        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
        //only throw the slice space key is pressed down and there is no slice in the air
        if (KeyDown == true && KeyHold == true && KeyUp == false && IsThrowing == 0)
        {
            Debug.Log("Space is pressed");
            AssignTag();
            // if(GlobalData.isFirstFusionOver==false)
            // {
            //     GameObject ui_handler = GameObject.Find("UIHandler");
            //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Great going! You pressed the space bar!"));
            // }
            // if (GlobalData.isFirstHorizontalFusionOver == false)
            // {
            //     GameObject ui_handler = GameObject.Find("UIHandler");
            //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Great going! You pressed the space bar!"));
            // }
            AddToList();
            ThrowSlice();

            StopRotation();
            GlobalData.previousSlice = gameObject.tag;
            Debug.Log("Here" + GlobalData.previousSlice);
            IsThrowing = 1;
            //Refresh the spawner and generate a new slice
            ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawnLevel0>()).NeedsNewSlice = 1;
        }
        if(IsThrowing == 1)
        {
            Animation += Time.deltaTime;
            if(Animation >= 1.3f)
            {

                IsThrowing = 0;
                IsPlaced = true;
                   if(IsBomb)
                {
                    Bomb();
                    return;
                }
                //If it is a color changer, change the color
                if(IsColorChanger)
                {
                    ChangeColor();
                    return;
                }
                //once the throw animation is completed, check the fuse
                FuseSliceLevel0.mHorizontalFuse();
                FuseSliceLevel0.mVertFuse(GlobalData.globalList[TargetList]);
                //if it is a bomb, do bomb
                
                
                if (GlobalData.globalList[TargetList].Count >= 6) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startWobble();
                }
                if (GlobalData.globalList[TargetList].Count >= 9) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 1.3f);
         
        }

        // if(IsPlaced == true && (GlobalData.isFirstFusionOver == false|| GlobalData.isFirstHorizontalFusionOver==false))
        // {
        //     GameObject ui_handler = GameObject.Find("UIHandler");
        //     ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("The pizza slice rotates! Stay put, and Wait for my instruction to throw!"));
        // }

        //else
        //{
        //    GameObject ui_handler = GameObject.Find("UIHandler");
        //    ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction(""));

        //}

    }


    bool isRotationToBeStopped(string tag)
    {
        string lastAnchor = GlobalData.previousSlice;
        Debug.Log("Last Anchor:" + lastAnchor);
        Debug.Log("Tag:" + tag);
        if(lastAnchor == "AnchorOne" && (tag== "R_1"|| tag == "Y_1"))
        {
            return true;
        }else if(lastAnchor == "AnchorTwo" && (tag == "R_2" || tag == "Y_2"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorThree" && (tag == "R_3" || tag == "Y_3"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorFour" && (tag == "R_4" || tag == "Y_4"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorFive" && (tag == "R_5" || tag == "Y_5"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorSix" && (tag == "R_6" || tag == "Y_6"))
        {
            return true;
        }

        return false;
}

    bool isRotationToBeStoppedForHorizontalFusion(string tag)
    {
        string lastAnchor = GlobalData.previousSlice;
        Debug.Log("Last Anchor:" + lastAnchor);
        Debug.Log("Tag:" + tag);
        if (lastAnchor == "AnchorOne" && (tag == "R_2" || tag == "Y_2"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorTwo" && (tag == "R_3" || tag == "Y_3"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorThree" && (tag == "R_4" || tag == "Y_4"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorFour" && (tag == "R_5" || tag == "Y_5"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorFive" && (tag == "R_6" || tag == "Y_6"))
        {
            return true;
        }
        else if (lastAnchor == "AnchorSix" && (tag == "R_1" || tag == "Y_1"))
        {
            return true;
        }
        return false;
    }
}
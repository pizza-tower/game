using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine. SceneManagement;

public class PizzaParabola : MonoBehaviour
{
    private int Level;
    // Start is called before the first frame update
    protected float Animation = 0;
    Vector3 StartPoint;
    Vector3 EndPoint;
    private float IsThrowing = 0;
    private string ListToAdd = "AnchorOne";
    private GameObject AnchorToFind;
    private SliceList List;
    bool IsPlaced = false;

    void Start()
    {
        StartPoint = (GameObject.FindWithTag("Spawner")).transform.position;
        Level=SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level : " + Level);
    }
    // Update is called once per frame
    void AddToList()
    {
        AnchorToFind = GameObject.FindWithTag(ListToAdd);       
        List = AnchorToFind.GetComponent<SliceList>();
        List.SList.Add(gameObject);
        
    }
    void StopRotation()
    {
        GetComponent<PizzaRotation>().StopRotate = 1;
    }

    void ResumeRotation()
    {
        GetComponent<PizzaRotation>().StopRotate = 0;
    }

    void ThrowSlice()
    {
        
        EndPoint = (GameObject.FindWithTag(ListToAdd)).transform.position;
        float Count = List.SList.Count;
        EndPoint.y += 0.15f *  Count;
            
    }
    void Update()
    {
        if(IsPlaced)
        {
            GlobalData.isFirstSlice = false;
            Debug.Log("First SLice done");
            return;
        }
        

        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
        
        string tag = gameObject.tag;

        // if level is 0, and not first slice
        //get prev tag 
        //stop rotation based on previous slice tag
        //set text to press space now
        //pause game until space is pressed

        if (Level==0 && GlobalData.isFirstSlice == false && GlobalData.isFirstFusionOver==false)
        {

            if(isRotationToBeStopped(tag))
            {
                StopRotation();
                Debug.Log("Rotation stopped");
                GameObject ui_handler = GameObject.Find("UIHandler");
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Press SPACE BAR NOW!!  Will the slices fuse? Lets see!"));
            }


        }


        if (tag == "R_1" || tag == "Y_1")
        {
            ListToAdd = "AnchorOne";
        }
        else if (tag == "R_2" || tag == "Y_2")
        {
            ListToAdd = "AnchorTwo";
        }
        else if (tag == "R_3" || tag == "Y_3")
        {
            ListToAdd = "AnchorThree";
        }
        else if (tag == "R_4" || tag == "Y_4")
        {
            ListToAdd = "AnchorFour";
        }
        else if (tag == "R_5" || tag == "Y_5")
        {
            ListToAdd = "AnchorFive";
        }
        else if (tag == "R_6" || tag == "Y_6")
        {
            ListToAdd = "AnchorSix";
        }



        //only throw the slice space key is pressed down and there is no slice in the air
        if (KeyDown == true && KeyHold == true && KeyUp == false && IsThrowing == 0)
        {
            Debug.Log("Space is pressed");
            if(GlobalData.isFirstFusionOver==false && Level==0)
            {
                GameObject ui_handler = GameObject.Find("UIHandler");
                ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("Great going! You pressed the space bar!"));
            }
            AddToList();
            ThrowSlice();
            StopRotation();
            GlobalData.previousSlice = ListToAdd;
            Debug.Log("Here" + GlobalData.previousSlice);
            IsThrowing = 1;
            //Refresh the spawner and generate a new slice
            ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawn>()).NeedsNewSlice = 1;
        }
        if(IsThrowing == 1)
        {
            Animation += Time.deltaTime;
            if(Animation >= 2.0f)
            {
                IsThrowing = 0;
                IsPlaced = true;
                FuseSlice.mVertFuse(List.SList,Level);
                FuseSlice.mHorizontalFuse(Level);
                if (List.SList.Count >= 6) {
                    AnchorToFind.GetComponent<Wobble>().startWobble();
                }
                if (List.SList.Count >= 9) {
                    AnchorToFind.GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 2f);
         
        }

        if(IsPlaced == true && GlobalData.isFirstFusionOver == false && Level==0)
        {
            GameObject ui_handler = GameObject.Find("UIHandler");
            ExecuteEvents.Execute<IPizzaTowerUIMessageTarget>(ui_handler, null, (x, y) => x.SetTutorialInstruction("The pizza slice rotates! Stay put, and Wait for my instruction to throw!"));
        }
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
}
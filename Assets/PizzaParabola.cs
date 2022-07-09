using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine. SceneManagement;
using UnityEngine.Analytics;

public class PizzaParabola : MonoBehaviour
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
 
        if(GetComponent<PizzaRotation>().TagInInt == 0)
        {
            if(GetComponent<PizzaRotation>().IsRed == 1)
            {
                gameObject.tag = "R_1";
            }
            else 
            {
                gameObject.tag = "Y_1";
            }
        }
        else if (GetComponent<PizzaRotation>().TagInInt == 1)
        {
            if(GetComponent<PizzaRotation>().IsRed == 1)
            {
                gameObject.tag = "R_2";
            }
            else 
            {
                gameObject.tag = "Y_2";
            }
        }
        else if (GetComponent<PizzaRotation>().TagInInt == 2)
        {
            if(GetComponent<PizzaRotation>().IsRed == 1)
            {
                gameObject.tag = "R_3";
            }
            else 
            {
                gameObject.tag = "Y_3";
            }
        }
        else if (GetComponent<PizzaRotation>().TagInInt == 3)
        {
            if(GetComponent<PizzaRotation>().IsRed == 1)
            {
                gameObject.tag = "R_4";
            }
            else 
            {
                gameObject.tag = "Y_4";
            }
        }
        else if (GetComponent<PizzaRotation>().TagInInt == 4)
        {
            if(GetComponent<PizzaRotation>().IsRed == 1)
            {
                gameObject.tag = "R_5";
            }
            else 
            {
                gameObject.tag = "Y_5";
            }
        }
        else if (GetComponent<PizzaRotation>().TagInInt == 5)
        {
            if(GetComponent<PizzaRotation>().IsRed == 1)
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
        FuseSlice.BombFuse(GlobalData.globalList[TargetList]);
        
        Destroy(gameObject);
    }
    void ChangeColor()
    {
        IsColorChanger = false;
        if(GlobalData.globalList[TargetList].Count >= 1)
        {
            GlobalData.globalList[TargetList][GlobalData.globalList[TargetList].Count - 1].GetComponent<Materials>().FlipColor();
        }
        FuseSlice.mHorizontalFuse();
        FuseSlice.mVertFuse(GlobalData.globalList[TargetList]);
        
        Destroy(gameObject);
    }
    void StopRotation()
    {
        GetComponent<PizzaRotation>().StopRotate = 1;
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

            AddToList();
            ThrowSlice();

            StopRotation();
            IsThrowing = 1;
            //Refresh the spawner and generate a new slice
            ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawn>()).NeedsNewSlice = 1;
        }
        if(IsThrowing == 1)
        {
            Animation += Time.deltaTime;
            if(Animation >= 1.3f)
            {
                Animation = 1.3f;
                
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
                int verticalFuseHappened = 1;
                while(verticalFuseHappened == 1)
                {
                    verticalFuseHappened = 0;
                    int r;
                    r = FuseSlice.mHorizontalFuse();
                    if (r != 0)
                    {
                        // Score calc
                    }
                    for(int i = 0; i < 6; i++)
                    {
                        r = FuseSlice.mVertFuse(GlobalData.globalList[i]);
                        if (r != 0)
                        {
                            verticalFuseHappened = 1;
                        }
                    }
                    
                }
                
                
                if (GlobalData.globalList[TargetList].Count >= 6) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startWobble();
                }
                if (GlobalData.globalList[TargetList].Count >= 9) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 1.3f);
         
        }

    }

}
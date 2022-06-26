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
    public bool IsBomb = false;
    public bool IsColorChanger = false;
    AnimationOnFuse animator;
    AnimationOnHalfFuse halfFuseAnimator;
    void Start()
    {
        StartPoint = (GameObject.FindWithTag("Spawner")).transform.position;
        animator = FindObjectOfType<AnimationOnFuse>();
        halfFuseAnimator = FindObjectOfType<AnimationOnHalfFuse>();

    }
    // Update is called once per frame
    void AddToList()
    {
        string tag = gameObject.tag;
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
        AnchorToFind = GameObject.FindWithTag(ListToAdd);       
        List = AnchorToFind.GetComponent<SliceList>();
        //only add the gameobject to the list when it is a slice
        if(IsBomb == false && IsColorChanger == false)
        {
            List.SList.Add(gameObject);
        }
        
        
        
    }
    void AssignTag()
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
        FuseSlice.BombFuse(List.SList);
        
        Destroy(gameObject);
    }
    void ChangeColor()
    {
        IsColorChanger = false;
        if(List.SList.Count >= 1)
        {
            List.SList[List.SList.Count - 1].GetComponent<Materials>().FlipColor();
        }
        FuseSlice.mHorizontalFuse();
        FuseSlice.mVertFuse(List.SList);
        
        Destroy(gameObject);
    }
    void StopRotation()
    {
        GetComponent<PizzaRotation>().StopRotate = 1;
    }

    void ThrowSlice()
    {
        
        EndPoint = (GameObject.FindWithTag(ListToAdd)).transform.position;
        float Count = List.SList.Count;
        EndPoint.y += 0.15f * Count;
            
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
            if(Animation >= 2.0f)
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
                FuseSlice.mHorizontalFuse();
                FuseSlice.mVertFuse(List.SList);
                //if it is a bomb, do bomb
                
                if (List.SList.Count >= 6) {
                    AnchorToFind.GetComponent<Wobble>().startWobble();
                }
                if (List.SList.Count >= 9) {
                    AnchorToFind.GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 2f);
            //do Animation on Horizontal fuse
            if (GlobalData.isHorizontalFuse)
            {
                Debug.Log("Horizontal Fusion Happened");
                Vector3 temp = new Vector3(0.0f, 0.0f, 0.0f);
                animator.animate(temp);
                GlobalData.isHorizontalFuse = false;
            }else if(GlobalData.isHorizontalHalfFuse)
            {
                Debug.Log("Half Horizontal Fusion Happened");
                Vector3 temp = new Vector3(0.0f, 0.0f, 0.0f);
                halfFuseAnimator.animate(temp);
                GlobalData.isHorizontalHalfFuse= false;
            }

        }

    }

}
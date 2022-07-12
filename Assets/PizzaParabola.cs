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
    Vector3 StartRotation;
    Vector3 EndRotation;
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

      
    }
    // Update is called once per frame
    void AddToList()
    {
        switch(GetComponent<PizzaRotation>().TagInInt)
        {
            case 0:
                TargetList = 0;
                TargetAnchor = "AnchorOne";
                break;
            case 1:
                TargetList = 1;
                TargetAnchor = "AnchorTwo";
                break;
            case 2:
                TargetList = 2;
                TargetAnchor = "AnchorThree";
                break;
            case 3:
                TargetList = 3;
                TargetAnchor = "AnchorFour";
                break;
            case 4:
                TargetList = 4;
                TargetAnchor = "AnchorFive";
                break;
            case 5:
                TargetList = 5;
                TargetAnchor = "AnchorSix";
                break;
        }
        
        //only add the gameobject to the list when it is a slice
        if(IsBomb == false && IsColorChanger == false)
        {
            GlobalData.globalList[TargetList].Add(gameObject);
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
        //Check if changing color caused a fusion
        FusionCheck();
        
        Destroy(gameObject);
    }
    void StopRotation()
    {
        GetComponent<PizzaRotation>().StopRotate = 1;
    }
    void ThrowSlice()
    {
        EndPoint = (GameObject.FindWithTag(TargetAnchor)).transform.position;
        EndRotation.y += (float)GetComponent<PizzaRotation>().TagInInt * (float)60.0;
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
            //StartRotation = transform.rotation;
            AddToList();
            ThrowSlice();
            StartPoint = transform.position;
            StopRotation();
            IsThrowing = 1;
            
        }
        if(IsThrowing == 1)
        {
            Animation += Time.deltaTime;
            if(Animation >= 1.3f)
            {
                Animation = 1.3f;
                IsThrowing = 0;
                IsPlaced = true;
                //Refresh the spawner and generate a new slice
                ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawn>()).NeedsNewSlice = 1;
                ((GameObject.FindWithTag("Peel")).GetComponent<PizzaPeelController>()).Reset();
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
                FusionCheck();
                
                if (GlobalData.globalList[TargetList].Count >= 6) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startWobble();
                }
                if (GlobalData.globalList[TargetList].Count >= 9) {
                    GameObject.FindWithTag(TargetAnchor).GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 1.3f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(EndRotation), 6 * Time.deltaTime);
        }

    }
    void FusionCheck()
    {
        int verticalFuseHappened = 1;
        while (verticalFuseHappened == 1)
        {
            verticalFuseHappened = 0;
            int r;
            FuseSlice.mHorizontalFuse();
            for (int i = 0; i < 6; i++)
            {
                r = FuseSlice.mVertFuse(GlobalData.globalList[i]);
                if (r != 0)
                {
                    verticalFuseHappened = 1;
                }
            }

        }
    }

}
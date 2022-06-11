using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaParabola : MonoBehaviour
{
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
            return;
        }
        

        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
        
        string Tag = gameObject.tag;

        if(tag == "R_1" || tag == "Y_1")
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
        if(KeyDown == true && KeyHold == true && KeyUp == false && IsThrowing == 0)
        {
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
                FuseSlice.mVertFuse(List.SList);
                FuseSlice.mHorizontalFuse();
                if (List.SList.Count >= 6) {
                    AnchorToFind.GetComponent<Wobble>().startWobble();
                }
                if (List.SList.Count >= 9) {
                    AnchorToFind.GetComponent<Wobble>().startFall();

                }

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 2f);
        }  

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFall : MonoBehaviour
{
    private int Level;
    // Start is called before the first frame update
    protected float Animation = 0;
    Vector3 StartPoint;
    Vector3 EndPoint;
    private float IsFalling = 0;
    private string ListToAdd = "AnchorOne";
    private GameObject AnchorToFind;
    private SliceList List;
    bool IsPlaced = false;
    public int WaitTimeBeforeFreeFall; 
    void Start()
    {
        StartPoint = transform.position;
        StartCoroutine(StartFreeFall());
    }
    IEnumerator StartFreeFall()
    {
        yield return new WaitForSeconds(WaitTimeBeforeFreeFall);
        IsFalling = 1;
        AddToList();
        ThrowSlice();

    }

    // Update is called once per frame
    void AddToList()
    {
        AnchorToFind = GameObject.FindWithTag(ListToAdd);
        List = AnchorToFind.GetComponent<SliceList>();
        List.SList.Add(gameObject);

    }
    void ThrowSlice()
    {

        EndPoint = (GameObject.FindWithTag(ListToAdd)).transform.position;
        float Count = List.SList.Count;
        EndPoint.y += 0.15f * Count;

    }
    void Update()
    {
        if (IsPlaced)
        {
            GlobalData.isFirstSlice = false;
            Debug.Log("First SLice done");
            return;
        }

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



        
        if (IsFalling == 1)
        {
            Animation += Time.deltaTime;
            if (Animation >= 2.0f)
            {
                IsFalling = 0;
                IsPlaced = true;
                FuseSlice.mVertFuse(List.SList, Level);
                FuseSlice.mHorizontalFuse(Level);

            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 1f, Animation / 2f);

        }

        

    }


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPeelController : MonoBehaviour
{
    // Start is called before the first frame update
    private int TagIndex = 0;
    string[] Anchors = new string[] {"AnchorOne", "AnchorTwo", "AnchorThree", "AnchorFour", "AnchorFive", "AnchorSix"};
    public string inputName; //name of the axis

    void Start()
    {
        StartCoroutine(TagIndexIncrement());
    }
    IEnumerator TagIndexIncrement()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            TagIndex += 1;
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //control the flipper with space bar
        if(Input.GetAxis(inputName) == 1)
        {
            GetComponent<HingeJoint>().useMotor = true;
        }
        else
        {
             GetComponent<HingeJoint>().useMotor = false;
        }
        if(GameObject.FindWithTag("0"))
        {
            StartCoroutine(Throw());
        }

        
    }
    IEnumerator Throw()
    {
        yield return new WaitForSeconds(1);
        GameObject newSlice = GameObject.FindWithTag("0"); //new spawn slice, tag 0
        int newTag = TagIndex%5 + 1;
        newSlice.tag = newTag.ToString();
        GameObject Anchor = GameObject.FindWithTag(Anchors[(TagIndex%6)]);
        Vector3 Anchor_pos = Anchor.transform.position;
        Vector3 Drop_pos = Anchor_pos;
        Drop_pos.y += 7; 
        newSlice.transform.position = Drop_pos;
        newSlice.transform.rotation = Anchor.transform.rotation;
        Anchor.GetComponent<Wobble>().AddSlice();
        
    }
}

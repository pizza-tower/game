using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaParabola : MonoBehaviour
{
    // Start is called before the first frame update
    protected float Animation = 0;
    Vector3 StartPoint;
    Vector3 EndPoint;
    public string inputName;
    private float IsThrowing = 0;
    void Start()
    {
        StartPoint = (GameObject.FindWithTag("Peel")).transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
        string Tag = gameObject.tag;
        if(tag == "1")
        {
            EndPoint = (GameObject.FindWithTag("AnchorOne")).transform.position;
        }
        else if (tag == "2")
        {
            EndPoint = (GameObject.FindWithTag("AnchorTwo")).transform.position;
        }
        else if (tag == "3")
        {
            EndPoint = (GameObject.FindWithTag("AnchorThree")).transform.position;
        }
        else if (tag == "4")
        {
            EndPoint = (GameObject.FindWithTag("AnchorFour")).transform.position;
        }
        else if (tag == "5")
        {
            EndPoint = (GameObject.FindWithTag("AnchorFive")).transform.position;
        }
        else if (tag == "6")
        {
            EndPoint = (GameObject.FindWithTag("AnchorSix")).transform.position;
        }
        EndPoint.y+= .2f;
        
        if(Input.GetAxis(inputName) == 1)
        {
            //once the flipper is active, stop the slice rotation
            IsThrowing = 1;
            GetComponent<PizzaRotation>().StopRotate = 1;
        }
        if(IsThrowing == 1)
        {
            Animation += Time.deltaTime;
            if(Animation >= 2.0f)
            {
                IsThrowing = 0;
            }
            transform.position = MathParabola.Parabola(StartPoint, EndPoint, 5f, Animation / 2f);
        }
    }
}

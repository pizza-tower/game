using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPeelController : MonoBehaviour
{
    //public string inputName;
    void Start()
    {
        
    }

    void Update()
    {
        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
	bool KeyUpMobile = Input.GetKeyUp(KeyCode.Tap);
        //control the flipper with space bar
        if(KeyDown && KeyHold && !KeyUp)
        {
            GetComponent<HingeJoint>().useMotor = true;
            
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<HingeJoint>().useMotor = false;
        }
        
       
    }
}

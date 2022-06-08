using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPeelController : MonoBehaviour
{
    //public string inputName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool KeyDown = Input.GetKeyDown(KeyCode.Space);
        bool KeyHold = Input.GetKey(KeyCode.Space);
        bool KeyUp = Input.GetKeyUp(KeyCode.Space); 
        //control the flipper with space bar
        if(KeyDown && KeyHold && !KeyUp)
        {
            GetComponent<HingeJoint>().useMotor = true;
            //Refresh the spawner and generate a new slice
            ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawn>()).NeedsNewSlice = 1;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<HingeJoint>().useMotor = false;
        }
        
       
    }
}
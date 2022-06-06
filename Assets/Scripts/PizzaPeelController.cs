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
        //control the flipper with space bar
        if(Input.GetKeyDown("space"))
        {
            GetComponent<HingeJoint>().useMotor = true;
            //Refresh the spawner and generate a new slice
            ((GameObject.FindWithTag("Spawner")).GetComponent<NewSliceSpawn>()).NeedsNewSlice = 1;
        }
        if(Input.GetKeyUp("space"))
        {
            GetComponent<HingeJoint>().useMotor = false;
        }
        
       
    }
}

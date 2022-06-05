using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public int slices;
    int IntegerTag;
    // Start is called before the first frame update
    void Start()
    {
        
        slices = 0;
        if (tag == "AnchorOne") {
            IntegerTag = 1;
        } 
        else if (tag == "AnchorTwo") {
            IntegerTag = 2;
        }
        else if (tag == "AnchorThree") {
            IntegerTag = 3;
        } 
        else if (tag == "AnchorFour") {
            IntegerTag = 4;
        }
        else if (tag == "AnchorFive") {
            IntegerTag = 5;
        } 
        else if (tag == "AnchorSix") {
            IntegerTag = 6;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slices >= 6 && slices < 9) {
            // wobble = GetComponent<Animation>();
            // wobble.Play();
            GameObject[] slices = GameObject.FindGameObjectsWithTag(IntegerTag.ToString());
            Animation wobble = GetComponent<Animation>();
            wobble.Play("Wobble");

            // print(slices);


        }
        else if (slices >= 9) {
            // GameObject[] slices = FindGameObjectsWithTag(tag);
        }
        
    }
    public void AddSlice() {
        slices += 1;
    }
}

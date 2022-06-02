using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChoosePile : MonoBehaviour
{
    [SerializeField] private GameObject slice;

    //Currently it is list of list of string. If colors are to be involved, it may be better to represent as an Object Item(color, label)
    //Fusing logic can use the color and label of the Item/slice
    List<List<string>> stacks = new List<List<string>>();

    int count=0;
    // Start is called before the first frame update
    void Start()
    {
        //we can remove this section once we integrate with flipper??
        GameObject stackbase = GameObject.FindWithTag("1");

        Vector3 base_pos = stackbase.transform.position;
        Vector3 spawn_pos = base_pos;
        spawn_pos.y += 20; 
        Instantiate(slice, spawn_pos, stackbase.transform.rotation);

        //since we are considering 6 slices
        initializeStacks(6);
    }

    void initializeStacks(int max_slice) {
        for(int i=0;i<max_slice;i++) {
            stacks.Add(new List<string>());
        }
        //adding the initial slice at 1 stack position  as given in  start() method
        stacks[0].Add("1");
    }


    //Replace this implementation with flipping pan team, Get the slice number 
    string getFromPanFlipper() {
        System.Random rnd =new System.Random();
        int random = rnd.Next(1,7);
        string randomtag=random.ToString();
        return randomtag;
    }

    // Update is called once per frame
    void Update()
    {

        count++;

        if(count<4) {
        // GetVal();
        String input = getFromPanFlipper();
        Debug.LogFormat("Tag is: " + input);
        GameObject stackbase = GameObject.FindWithTag(input);
        Vector3 base_pos = stackbase.transform.position;
        Vector3 spawn_pos = base_pos;

        int index = Int32.Parse(input)-1;
        List<String> currentStack= stacks[index];

        //to get the new y value
        int height=currentStack.Count*20 + 20;
        Debug.LogFormat("stack label" + index+ "stack count" + currentStack.Count);

        spawn_pos.y = height;

        //position at which pizza lands
        //Currently just setting to pizza slice number i.e. randomly generated value
        //Update it when rotation functionality is involved
        // GameObject targetPosition= GameObject.FindWithTag(input);

        Instantiate(stackbase, spawn_pos, stackbase.transform.rotation);
        
        stacks[index].Add(input);

        //no operation on stack now. Update it later.
        fuse();        
     
        }  
    }


    void fuse(){
        //add fuse logic here
        //go through the list of lists i.e. stacks here to fuse vertically or horizontally
        //horizontal fuse can happen at any layer i.e. at any index.
    }
}
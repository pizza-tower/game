using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public int slices;
    // Start is called before the first frame update
    void Start()
    {
        slices = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSlice() {
        slices += 1;
    }
}

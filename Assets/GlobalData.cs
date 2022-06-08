using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    
    public static List<GameObject> col1 = new List<GameObject>();

    public static int numSlicesFused=0;
    public static int verticalFusionHeight=2;
    public static List<List<string>> columnSliceList = new List<List<string>>{new List<string>{"y","r","y","y"},new List<string>{"r"}};

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

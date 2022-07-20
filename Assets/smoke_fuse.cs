using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke_fuse : MonoBehaviour
{
    public GameObject smoke;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalData.verticalFuse)
        {
            GameObject smoke_vfx = Instantiate(smoke,new Vector3(4,-1,-12),Quaternion.Euler(new Vector3(90,-60,90)));
            smoke_vfx.transform.localScale = new Vector3(3,3,3);
            smoke_vfx.GetComponent<Renderer>().material.color = Color.red;
            Destroy(smoke_vfx,1);
            GlobalData.verticalFuse = false;
        }

        if(GlobalData.horizontalFuse)
        {
            GameObject smoke_vfx = Instantiate(smoke,new Vector3(4,-1,-12),Quaternion.identity);
            smoke_vfx.transform.localScale = new Vector3(3,3,3);
            Destroy(smoke_vfx,1);
            GlobalData.horizontalFuse = false;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSlice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Throw());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Throw()
    {
        yield return new WaitForSeconds(2);
        GameObject AnchorOne = GameObject.FindWithTag("1");
        
        Vector3 base_pos = AnchorOne.transform.position;
        Vector3 spawn_pos = base_pos;
        spawn_pos.y += 20; 
        transform.position = spawn_pos;
    }
}

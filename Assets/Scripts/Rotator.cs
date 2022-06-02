using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rotator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator rotator()
    {
        yield return new WaitForSeconds(2);
        GameObject stackbase = GameObject.FindWithTag("1");
        Vector3 base_pos = stackbase.transform.position;
        Vector3 spawn_pos = base_pos;
        spawn_pos.y += 20; 
        transform.position = spawn_pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSliceSpawner : MonoBehaviour
{
    public GameObject pizzaslicePrefab;
    void Start()
    {
        
    }
    public void spawnSlice() 
    {
        GameObject s = Instantiate(pizzaslicePrefab) as GameObject;
        s.transform.position = transform.position;
        //s.transform.rotation = transform.rotation;
    }
    // Update is called once per frame
    //IEnumerator sliceFold()
    void Update()
    {
        if(Input.GetButtonDown("PizzaPeelFlip"))
        {
            StartCoroutine(waiter());//wait for 1.5 s after the buttton pressed down
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);//wait for 1.5s to spawn the new slice
        spawnSlice();
    }

}

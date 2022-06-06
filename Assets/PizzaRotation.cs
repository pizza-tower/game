using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotation : MonoBehaviour
{
    private int TagIndex = 0;
    string[] Tags = new string[] {"1", "2", "3", "4", "5", "6"};
    public int IsRotating = 1;
    public int StopRotate = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRotating == 1  && StopRotate == 0)
        {
            StartCoroutine(Rotate());
        }
    }
    IEnumerator Rotate()
    {
        IsRotating = 0;
        TagIndex += 1;
        transform.Rotate(0, 60, 0);
        gameObject.tag = Tags[(TagIndex%6)];
        yield return new WaitForSeconds(1);
        IsRotating = 1;
    }
}

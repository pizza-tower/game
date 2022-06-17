using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotation : MonoBehaviour
{
    private int TagIndex = 0;
    string[] RedTags = new string[] {"R_1", "R_2", "R_3", "R_4", "R_5", "R_6"};
    string[] YellowTags = new string[] {"Y_1", "Y_2", "Y_3", "Y_4", "Y_5", "Y_6"};
    public int IsRotating = 1;
    public int StopRotate = 0;
    public int hardcoded = 5;
    [SerializeField] private Material myMaterial;
    public int IsRed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRotating == 1  && StopRotate == 0 && hardcoded != 5)
        {
            StartCoroutine(Rotate());
        }
    }
    IEnumerator Rotate()
    {
        IsRotating = 0;
        TagIndex += 1;
        transform.Rotate(0, 60, 0);
        if(IsRed == 1)
        {
            gameObject.tag = RedTags[(TagIndex%6)];
        } 
        else 
        {
            gameObject.tag = YellowTags[(TagIndex%6)];
        }

        
        yield return new WaitForSeconds(1);
        IsRotating = 1;
    }
}
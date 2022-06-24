using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRotationAutoFlipper : MonoBehaviour
{
    private int TagIndex = 0;
    string[] RedTags = new string[] { "R_1", "R_2", "R_3", "R_4", "R_5", "R_6" };
    string[] YellowTags = new string[] { "Y_1", "Y_2", "Y_3", "Y_4", "Y_5", "Y_6" };
    public int IsRotating = 1;
    public int StopRotate = 0;
    [SerializeField] private Material myMaterial;
    public int IsRed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsRotating == 1 && StopRotate == 0)
        {
            //StartCoroutine(Rotate());
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("ROTATE");
                Rotate();
            }
        }
    }
    void Rotate()
    {
        IsRotating = 0;
        TagIndex += 1;
        transform.Rotate(0, 60, 0);
        if (IsRed == 1)
        {
            gameObject.tag = RedTags[(TagIndex % 6)];
        }
        else
        {
            gameObject.tag = YellowTags[(TagIndex % 6)];
        }
        // yield return new WaitForSeconds(1);
        StartCoroutine(wait());
    }


    IEnumerator wait()
    {
        Debug.Log("Start wait");
        yield return new WaitForSeconds(0.1F);
        IsRotating = 1;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFlip : MonoBehaviour
{
    [SerializeField] [Range(0f, 12f)] float lerpRotationTime;
    [SerializeField] Vector3[] myFlipAngles;
    [SerializeField] Vector3[] myThrowAngles;

    public int throwIt = 0;
    int flipAngleIndex; 
    int ThrowAngleIndex = 0;
    int lenFlip;
    float tFlip = 0f;
    float tThrow = 0f;
    // Start is called before the first frame update
    void Start()
    {
        lenFlip = myFlipAngles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //actual throwing pizza slice animation
        if(throwIt == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myThrowAngles[ThrowAngleIndex]), lerpRotationTime * Time.deltaTime);
            tThrow= Mathf.Lerp(tThrow, 1f, lerpRotationTime * Time.deltaTime);
            if(tThrow>0.9f)
            {
                tThrow = 0f;
                //ThrowAngleIndex += 1;
                //ThrowAngleIndex %= 1;
                throwIt = 0;
                flipAngleIndex = 0;
                tFlip = 0;
            }
            return;
        }
        //normal flip animation, flip the peel to rotate the pizza slices
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myFlipAngles[flipAngleIndex]), lerpRotationTime * Time.deltaTime);
        tFlip= Mathf.Lerp(tFlip, 1f, lerpRotationTime * Time.deltaTime);
        if(tFlip>0.9f)
        {
            tFlip = 0f;
            flipAngleIndex += 1;
            flipAngleIndex %= 3;
        }
    }
}
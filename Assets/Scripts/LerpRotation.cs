using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    [SerializeField] [Range(0f, 6f)] float lerpRotationTime;
    [SerializeField] [Range(0f, 8f)] float lerpPositionTime;
    [SerializeField] Vector3[] myAngles;
    [SerializeField] Vector3[] myPositions;

    int angleIndex;
    int positionIndex; 

    int lenAng;
    int lenPos;
    float t1 = 0f;
    float t2 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lenAng = myAngles.Length;
        lenPos = myPositions.Length;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myPositions[positionIndex], lerpPositionTime * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myAngles[angleIndex]), lerpRotationTime * Time.deltaTime);


        t1= Mathf.Lerp(t1, 1.0f, lerpPositionTime * Time.deltaTime);
        t2= Mathf.Lerp(t2, 1.0f, lerpRotationTime * Time.deltaTime);
        if(t1>0.9f)
        {
            t1 = 0f;
            
            positionIndex += 1;
            positionIndex %= 18;
        }
        if(t2>0.9f)
        {
            t2 = 0f;
            angleIndex += 1;
            angleIndex %= 18;
        }
    }
}

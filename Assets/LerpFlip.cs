using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFlip : MonoBehaviour
{
    [SerializeField] [Range(0f, 12f)] float lerpRotationTime;
    [SerializeField] Vector3[] myAngles;

    int angleIndex; 

    int len;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        len = myAngles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myAngles[angleIndex]), lerpRotationTime * Time.deltaTime);
        //t2 += Time.deltaTime;
        
        //elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

        t= Mathf.Lerp(t, 1f, lerpRotationTime * Time.deltaTime);
        if(t>0.9f)
        {
            t = 0f;
            angleIndex += 1;
            angleIndex %= 3;
        }
    }
}

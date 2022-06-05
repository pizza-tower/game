using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateSlice : MonoBehaviour
{

    public GameObject pizzaslicePrefab;
    string[] Anchors = new string[] { "AnchorOne", "AnchorTwo", "AnchorThree", "AnchorFour", "AnchorFive", "AnchorSix" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);

        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            print(rotation);
            String tag = assignTags(rotation.y);
            Debug.Log("Tag:" + tag);
            dropToPositionAccordingToTag(tag);
            spawnSlice();

        }
    }

    void spawnSlice()
    {
        //Instantiate new game object in the position of old game object before dropping
    }

    void dropToPositionAccordingToTag(String tag)
    {
        //merge with others code
    }

    String assignTags(float degree)
    {

        //Y axis degrees changes this way - 0..180..-179...-120..0
        //After converting to Euler angles, degree  changes from 0 to 360
        if (degree>=0 && degree<60)
        {
          return Anchors[0];
        }
        else if (degree >= 60 && degree < 120)
        {
            return Anchors[1];
        }
        else if (degree >= 120 && degree < 180)
        {
            return Anchors[2];
        }
        else if (degree >= 180 && degree < 270)
        {
            return Anchors[4];
        }
        else 
        {
            return Anchors[5];
        }
   
    }

    //IEnumerator waiter()
    //{
    //    yield return new WaitForSeconds(1);//wait for 1.5s to spawn the new slice
    //    initialSpawnSlice();
    //}

    //public void spawnSlice(Vector3 position)
    //{
    //    GameObject s = Instantiate(pizzaslicePrefab) as GameObject;
    //    s.transform.position = position;
    //    gameObject.GetComponent<Rigidbody>().useGravity = false;
    //    //s.transform.rotation = transform.rotation;
    //}

    //public void initialSpawnSlice()
    //{
    //    GameObject s = Instantiate(pizzaslicePrefab) as GameObject;
    //    s.transform.position = transform.position;
    //    //s.transform.rotation = transform.rotation;
    //}


}

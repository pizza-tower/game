using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackControl : MonoBehaviour
{
    //bool isMoving = true;
    // Start is called before the first frame update
<<<<<<< HEAD
    void Start()
    {
        
=======
    
    void Start()
    {
       
>>>>>>> origin/Thomas-new
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "R_1" || col.gameObject.tag == "Y_1"  || col.gameObject.tag == "AnchorOne" ) {
            // isMoving = false;
            // print("happening");
            wait();
            // GameObject AnchorOne = GameObject.FindWithTag("AnchorOne");
            // transform.rotation = col.gameObject.transform.rotation;
            // Vector3 newPos = AnchorOne.transform.position;
            // newPos.y = transform.position.y;
            // // transform.position = newPos;
            // GetComponent<Rigidbody>().MovePosition(newPos);
            // GetComponent<Rigidbody>().isKinematic = true;



        }
    }
    IEnumerator wait() {
        yield return new WaitForSeconds(2);

    }
    // void FixedUpdate() {
    //         if (GetComponent<Rigidbody>().isKinematic == true && isMoving == true) {
    //             Vector3 pos = transform.position;
    
    //             Vector3 placePosition = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y - .5f), Mathf.Round(pos.z));
        
    //             transform.position = placePosition;
    //         }

    // }
}

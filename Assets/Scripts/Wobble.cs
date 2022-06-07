using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public int slices;
    int IntegerTag;
    PhysicMaterial frictionControl;
    bool wobbling = false;
    bool backward = false;
    Vector3 orig_pos;
    // Start is called before the first frame update
    void Start()
    {          
        orig_pos = transform.position;
        for (int i = 6; i <= 11; i++) {
                if (i != gameObject.layer) {
                    Physics.IgnoreLayerCollision(gameObject.layer, i);

                }
            }
        
        slices = 0;
        if (tag == "AnchorOne") {
            IntegerTag = 1;
        } 
        else if (tag == "AnchorTwo") {
            IntegerTag = 2;
        }
        else if (tag == "AnchorThree") {
            IntegerTag = 3;
        } 
        else if (tag == "AnchorFour") {
            IntegerTag = 4;
        }
        else if (tag == "AnchorFive") {
            IntegerTag = 5;
        } 
        else if (tag == "AnchorSix") {
            IntegerTag = 6;
        }
        frictionControl = GetComponent<MeshCollider>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slices >= 6 && slices < 9) {
            // wobble = GetComponent<Animation>();
            // wobble.Play();
            Animation wobble = GetComponent<Animation>();
            for (int i = 6; i <= 11; i++) {
                if (i != gameObject.layer) {
                    Physics.IgnoreLayerCollision(gameObject.layer, i);

                }
            }
            // wobble.Play("Wobble");
            wobbling = true;

            // print(slices);


        }
        else if (slices >= 9) {
            GameObject[] slices = GameObject.FindGameObjectsWithTag(IntegerTag.ToString());
            foreach (GameObject slice in slices) {
                if (slice != gameObject) {
                    slice.GetComponent<Rigidbody>().mass = 1;
                }
            }
        }
        
    }

    void FixedUpdate() {
        if (wobbling) {
            if (!backward) {
                Vector3 newpos = transform.position;
                newpos.x += .002f;
                transform.position = newpos;
                if (transform.position.x >= orig_pos.x + .02f) {
                    backward = true;
                }
            }
            else {
                Vector3 newpos = transform.position;
                newpos.x -= .002f;
                transform.position = newpos;
                if (transform.position.x <= orig_pos.x - .02f) {
                    backward = false;
                }
            }


        }
    }
    public void AddSlice() {
        slices += 1;
    }

}

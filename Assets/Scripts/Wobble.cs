using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public int slices;
    PhysicMaterial frictionControl;
    bool wobbling = false;
    bool backward = false;
    bool falling = false;
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
        frictionControl = GetComponent<MeshCollider>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        slices = GetComponent<SliceList>().SList.Count;
        // if (slices >= 6 && slices < 9) {
        //     // wobble = GetComponent<Animation>();
        //     // wobble.Play();
        //     Animation wobble = GetComponent<Animation>();
        //     for (int i = 6; i <= 11; i++) {
        //         if (i != gameObject.layer) {
        //             Physics.IgnoreLayerCollision(gameObject.layer, i);

        //         }
        //     }
        //     // wobble.Play("Wobble");
        //     wobbling = true;

        //     // print(slices);


        // }
        // else if (slices >= 9) {
        //     falling = true;
        //     wobbling = false;
        // }
        
    }
    public void startWobble() {
        wobbling = true;
    }
    public void startFall() {
        falling = true;
    }

    void FixedUpdate() {
        if (wobbling) {
            if (!backward) {
                Vector3 new_basepos = transform.position;
                new_basepos.x += .007f;
                transform.position = new_basepos;
                if (transform.position.x >= orig_pos.x + .04f) {
                    backward = true;
                }
                foreach (GameObject slice in GetComponent<SliceList>().SList) {
                    Vector3 newpos = slice.transform.position;
                    newpos.x += .007f;
                    slice.transform.position = newpos;
                    if (slice.transform.position.x >= orig_pos.x + .04f) {
                        backward = true;
                    }
                }

            }
            else {
                    Vector3 new_basepos = transform.position;
                    new_basepos.x -= .007f;
                    transform.position = new_basepos;
                    if (transform.position.x <= orig_pos.x - .04f) {
                        backward = false;
                    }
                foreach (GameObject slice in GetComponent<SliceList>().SList) {
                    Vector3 newpos = slice.transform.position;
                    newpos.x -= .007f;
                    slice.transform.position = newpos;
                    if (slice.transform.position.x <= orig_pos.x - .04f) {
                        backward = false;
                    }
                }
            }


        }
        if (falling) {
            wobbling = false;
            print("falling");
            foreach (GameObject slice in GetComponent<SliceList>().SList) {
                // slice.GetComponent<MeshCollider>().isTrigger = true;
                Vector3 newpos = slice.transform.position;
                newpos.y -= .05f;
                slice.transform.position = newpos;
            }
        }
    }

    public void AddSlice() {
        slices += 1;
    }


}

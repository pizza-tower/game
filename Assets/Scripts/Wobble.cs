using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public int slices;
    public int Anchor;
    PhysicMaterial frictionControl;
    bool wobbling = false;
    bool backward = false;
    bool falling = false;
    Vector3 orig_pos;
    float timeStep = 0.001f;
    float slideStep = 2.0f;
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
        if(GlobalData.globalList.Count > 0){
            slices = GlobalData.globalList[Anchor].Count;
            if (slices < 6) {
                wobbling = false;
            }
        }
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
                if (transform.position.x >= orig_pos.x + .09f) {
                    backward = true;
                }
                foreach (GameObject slice in GlobalData.globalList[Anchor]) {
                    Vector3 newpos = slice.transform.position;
                    newpos.x += .007f;
                    slice.transform.position = newpos;
                }
                if (GlobalData.globalList[Anchor][0].transform.position.x >= orig_pos.x + .09f) {
                    backward = true;
                }

            }
            else {
                    Vector3 new_basepos = transform.position;
                    new_basepos.x -= .007f;
                    transform.position = new_basepos;
                    if (transform.position.x <= orig_pos.x - .09f) {
                        backward = false;
                    }
                foreach (GameObject slice in GlobalData.globalList[Anchor]) {
                    Vector3 newpos = slice.transform.position;
                    newpos.x -= .007f;
                    slice.transform.position = newpos;
                }
                if (GlobalData.globalList[Anchor][0].transform.position.x <= orig_pos.x - .09f) {
                    backward = false;
                }
            }


        }
        if (falling) {
            wobbling = false;
            StartCoroutine(EndLevel());

            // Set horizontal shift with respect to anchor
            float xShift = 0.0f;
            float zShift = -0.02f;
            if (Anchor == 1) {
                xShift = -0.02f;
                zShift = -0.002f;
            }
            else if (Anchor == 2) {
                xShift = -0.02f;
                zShift = 0.001f;
            }
            else if (Anchor == 5) {
                xShift = 0.02f;
                zShift = -0.002f;
            }
            else if (Anchor == 3) {
                zShift = 0.02f;
            }
            else if (Anchor == 4) {
                xShift = 0.02f;
                zShift = 0.001f;
            }

            int sliceCounter = 1;
            foreach (GameObject slice in GlobalData.globalList[Anchor]) {
                if (sliceCounter >= 4) {
                    // slice.GetComponent<MeshCollider>().isTrigger = true;
                    Vector3 newpos = slice.transform.position;
                    if (newpos.y > (-1.19 + (sliceCounter * 0.1))) {
                        newpos.x += (xShift * sliceCounter) * timeStep * 2.1f;
                        newpos.z += (zShift * sliceCounter) * timeStep * 2.1f;
                        slice.transform.position = newpos;
                        slice.transform.Rotate((-0.1f * timeStep * 3), 0.0f, 0.0f, Space.Self);
                        slice.transform.position += new Vector3(0, 
                                                        (-0.01f * sliceCounter / 1.6f) * timeStep, 0);
                    }
                    else if (slideStep > 0) {
                        newpos.x += (xShift * sliceCounter) * slideStep;
                        newpos.z += (zShift * sliceCounter) * slideStep;
                        slice.transform.position = newpos;
                        slideStep -= 0.02f;
                    }
                }
                else if ((sliceCounter == 3 || sliceCounter == 2) && (timeStep < 2)) {
                    Vector3 newpos = slice.transform.position;
                    newpos.x += xShift * sliceCounter * timeStep / 2;
                    newpos.z += zShift * sliceCounter * timeStep / 2;
                    slice.transform.position = newpos;
                    slice.transform.Rotate(-0.1f, 0.0f, 0.0f, Space.Self);
                }

                sliceCounter += 1;
            }
            if (timeStep < 2) {
                timeStep += (timeStep / 4);
            }
        }
    }

    IEnumerator EndLevel() {
        yield return new WaitForSeconds(1);
        GlobalData.gameover = true;

    }

    public void AddSlice() {
        slices += 1;
    }


}

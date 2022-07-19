using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ArrowAnimation : MonoBehaviour
{
    private bool isActive=true;
    // GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        // obj = GameObject.Find("Arrows");
    }

    IEnumerator Animator(){
        
        yield return new WaitForSeconds(1);
        
        // gameObject.SetActive(isActive);
        if(isActive)
            gameObject.transform.localScale = new Vector3(15f, 15f, 15f);
        else
            gameObject.transform.localScale = new Vector3(10f, 10f, 10f);

        isActive=!isActive;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isActive);
        StartCoroutine(Animator());
    }
}


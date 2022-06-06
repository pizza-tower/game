using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public bool onoff = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle() {
        onoff = !onoff;
        // show or hide game over section
    }

    public void Restart() {
        SceneManagemer.LoadScene("SampleScene")
    }
}

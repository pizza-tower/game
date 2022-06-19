using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI score = GetComponent<TextMeshProUGUI>();
        score.SetText("Score: {0}", Score.CurrentScore);
    }
}

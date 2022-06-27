using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject SliceOnPeel;
    public static int RewardsCurrency = 2; 
    void Start()
    {
        
    }
    public static void EarnCurrency()
    {
        RewardsCurrency += 1;
    }
    public void LaunchBomb()
    {
        //Debug.Log($"Rewards Currency {RewardsCurrency}");
        if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabola>().IsBomb = true;
        SliceOnPeel.GetComponent<Materials>().ToBomb();
    }
    public void LaunchColorChanger()
    {
        if(RewardsCurrency < 1)
        {
            return;
        }
        RewardsCurrency -= 1;
        SliceOnPeel = GameObject.FindWithTag("0");
        SliceOnPeel.GetComponent<PizzaParabola>().IsColorChanger = true;
        SliceOnPeel.GetComponent<Materials>().ToRainbow();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
